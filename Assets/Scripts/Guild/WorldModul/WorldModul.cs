using System.Collections.Generic;
using UnityEngine;
using MapShape;
using System;

public class WorldModul : MonoBehaviour, ICanSkipTime
{
    [SerializeField] private List<Biom> _possibleBioms;

    [SerializeField] private Biom defaultBiom;
    [SerializeField] private SettelmentGenerator _settelmentGenerator;

    private List<WorldObject> _objects;

    private CenterBlock _centerBlock;
    private BiomLayer _layer;
    private PathFiender _pathFiender;

    public Action<ISettelmentQuestGiver> QuestAppear;

    public bool GetBiomFromPoint(Vector2 point, out Biom biomToDraw)
    {
        biomToDraw = defaultBiom;

        if (IslandShape.VerifyPoint(point))
        {
            if (_layer.GetBiomFromPoint(point, out Biom biom))
                biomToDraw = biom;

            return true;
        }

        return false;
    }

    public void GenerateWorld()
    {
        List<Vector2> settelmentPosiitions = new List<Vector2>();
        _objects = new List<WorldObject>();

        foreach (Settelment settelment in _settelmentGenerator.GenerateStartingSettelment()) 
        { 
            AddSettelment(settelment);
            settelmentPosiitions.Add(settelment.Position);
        }

        _layer = new BiomGenerator().Generate(_possibleBioms);
        _pathFiender =  new PathFiender(_layer, settelmentPosiitions);
    }

    public void SkipTime(float time)
    {
        foreach (WorldObject obj in _objects)
        {
            obj.Update(time);
        }
    }

    public void AddGroup(Group group)
    {

    }

    private void AddSettelment(Settelment settelment)
    {
        settelment.QuestAppeared += QuestAppear;

        _objects.Add(settelment);
    }

    private void DeleteSettelment(Settelment settelment)
    {
        settelment.QuestAppeared -= QuestAppear;

        DeleteWorldObject(settelment);
    }

    private void DeleteGroup()
    {

    }

    private void DeleteWorldObject(WorldObject worldObject)
    {
        _objects.Remove(worldObject);
    }
}
