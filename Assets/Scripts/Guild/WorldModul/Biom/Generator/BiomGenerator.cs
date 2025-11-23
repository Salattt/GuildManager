using System.Collections.Generic;
using UnityEngine;
using MapCfg;

public class BiomGenerator
{
    private int _maxLayerLvl = 1;
    private int _shapesToLayer = MapConfig.ShapesToLayer;
    private float _minShapeScale = MapConfig.MinShapeScaleSize;
    private float _maxShapeScale = MapConfig.MaxShapeScaleSize;
    private float _maxTurningAngle = MapConfig.MaxShapeTurningAngle;

    public BiomLayer Generate(List<Biom> possibleBioms)
    {
        List<TurnebleShape> hazardLayer = GenerateRandomWorldLayer();
        List<TurnebleShape> visibilityHardnessLayer = GenerateRandomWorldLayer();
        List<TurnebleShape> travelHardnessLayer = GenerateRandomWorldLayer();
        List<TurnebleShape> fogLayer = CreateFogLayer();

        return GenerateBiomsFromLayers(hazardLayer, visibilityHardnessLayer, travelHardnessLayer, fogLayer, possibleBioms);
    }

    private BiomLayer GenerateBiomsFromLayers(List<TurnebleShape> hazardLayer, List<TurnebleShape> visibilityHardnessLayer,
        List<TurnebleShape> travelHardnessLayer, List<TurnebleShape> fogLayer, List<Biom> possibleBioms)
    {
        BiomLayer biomLayer = new BiomLayer();

        foreach (BiomShapeHolder shapeHolder in CreateShapeHolders(hazardLayer,visibilityHardnessLayer,travelHardnessLayer,fogLayer,possibleBioms))
        {
            biomLayer.AddBiom(shapeHolder);
        }

        return biomLayer;
    }

    private List<TurnebleShape> GenerateRandomWorldLayer()
    {
        List<TurnebleShape> newShapes = new List<TurnebleShape>();

        for (int i = 0; i < _shapesToLayer; i++)
        {
            newShapes.Add(RandomShapeGenerator.GenerateRandomShape(Random.Range(1, _maxLayerLvl + 1), Random.Range(0, _maxTurningAngle),
                new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(_minShapeScale, _maxShapeScale)));
        }

        return newShapes;
    }

    private List<TurnebleShape> CreateFogLayer()
    {
        return new List<TurnebleShape> {new Circle(1,0,new Vector2(-1,0),0.5f)};

        // new Circle(2, 0, new Vector2(-1, 0), 0.5f), new Circle(3, 0, new Vector2(-1, 0), 0.25f)
    }

    private bool GetMaxLvlShape(Vector2 point, List<TurnebleShape> shapes, out TurnebleShape maxLvlShape)
    {
        bool isShapeFiended = false;
        int maxLvlFiended = 0;

        maxLvlShape = null;

        foreach (TurnebleShape shape in shapes)
        {
            if (shape.CheckPointInclude(point) && shape.Level > maxLvlFiended)
            {
                isShapeFiended = true;
                maxLvlFiended = shape.Level;
                maxLvlShape = shape;
            }
        }

        return isShapeFiended;
    }

    private List<Vector2> GetStructurePoints(List<TurnebleShape> turnebleShapes)
    {
        List<Vector2> points = new List<Vector2>();

        foreach (TurnebleShape shape in turnebleShapes)
        {
            points.AddRange(shape.GetShapeStructurePoints());
        }

        return points;
    }

    private List<BiomShapeHolder> CreateShapeHolders(List<TurnebleShape> hazardLayer, List<TurnebleShape> visibilityHardnessLayer,
        List<TurnebleShape> travelHardnessLayer, List<TurnebleShape> fogLayer, List<Biom> possibleBioms)
    {
        List<BiomShapeHolder> shapeHolders = new List<BiomShapeHolder>();
        List<Vector2> checkPoints = new List<Vector2>();

        checkPoints.AddRange(GetStructurePoints(hazardLayer));
        checkPoints.AddRange(GetStructurePoints(visibilityHardnessLayer));
        checkPoints.AddRange(GetStructurePoints(travelHardnessLayer));
        checkPoints.AddRange(GetStructurePoints(fogLayer));

        int hazardLvl;
        int visibilityHardnessLvl;
        int travelHardnessLvl;
        int fogLvl;

        for (int i = 0; i < checkPoints.Count; i++)
        {
            bool isHazardShapeFiended = GetMaxLvlShape(checkPoints[i], hazardLayer, out TurnebleShape hazardLayerShape);
            bool isVisibilityHardnessShapeFiended = GetMaxLvlShape(checkPoints[i], visibilityHardnessLayer, out TurnebleShape visibilityHardnessLayerShape);
            bool isTravelHardnessShapeFiended = GetMaxLvlShape(checkPoints[i], travelHardnessLayer, out TurnebleShape travelHardnessLayerShape);
            bool isFogShapeFiended = GetMaxLvlShape(checkPoints[i], fogLayer, out TurnebleShape fogLayerShape);
            bool isBiomFiended = false;

            for (int j = i + 1; j < checkPoints.Count; j++)
            {
                GetMaxLvlShape(checkPoints[j], hazardLayer, out TurnebleShape hazardLayerShapeToCompare);
                GetMaxLvlShape(checkPoints[j], visibilityHardnessLayer, out TurnebleShape visibilityHardnessLayerShapeToCompare);
                GetMaxLvlShape(checkPoints[j], travelHardnessLayer, out TurnebleShape travelHardnessLayerShapeToCompare);
                GetMaxLvlShape(checkPoints[j], fogLayer, out TurnebleShape fogLayerShapeToCompare);

                if(ReferenceEquals(hazardLayerShape ,hazardLayerShapeToCompare) && ReferenceEquals( visibilityHardnessLayerShape ,visibilityHardnessLayerShapeToCompare) &&
                    ReferenceEquals(travelHardnessLayerShape, travelHardnessLayerShapeToCompare) && ReferenceEquals(fogLayerShape, fogLayerShapeToCompare))
                {
                    checkPoints.RemoveAt(j);

                    j--;
                }
            }

            if (isHazardShapeFiended == false && isVisibilityHardnessShapeFiended == false && isTravelHardnessShapeFiended == false && isFogShapeFiended == false)
            {
                checkPoints.RemoveAt(i);
                i--;
                isBiomFiended = true;
            }
            else
            {
                if (isHazardShapeFiended)
                    hazardLvl = hazardLayerShape.Level;
                else
                    hazardLvl = 0;

                if (isVisibilityHardnessShapeFiended)
                    visibilityHardnessLvl = visibilityHardnessLayerShape.Level;
                else
                    visibilityHardnessLvl = 0;

                if (isTravelHardnessShapeFiended)
                    travelHardnessLvl = travelHardnessLayerShape.Level;
                else
                    travelHardnessLvl = 0;

                if (isFogShapeFiended)
                    fogLvl = fogLayerShape.Level;
                else
                    fogLvl = 0;

                foreach (Biom biom in possibleBioms)
                {
                    if (biom.HazardLvl == hazardLvl && biom.VisibilityHardnessLvl == visibilityHardnessLvl &&
                        biom.TravelHardnessLvl == travelHardnessLvl && biom.FogLvl == fogLvl)
                    {
                        isBiomFiended = true;
                        shapeHolders.Add(new BiomShapeHolder(biom, new OverlayShapes(new List<TurnebleShape> { hazardLayerShape, visibilityHardnessLayerShape,
                        travelHardnessLayerShape, fogLayerShape})));
                        break;
                    }
                }

                if (isBiomFiended == false)
                    throw new System.Exception($"biom not founded, h -{hazardLvl}, v -{visibilityHardnessLvl}, t -{travelHardnessLvl}, f -{fogLvl}");
            }
        }

        return shapeHolders;
    }
}