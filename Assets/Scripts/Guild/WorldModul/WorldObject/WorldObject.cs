using UnityEngine;
using System;

public abstract class WorldObject
{
    protected Vector2 Position { get; }

    public string Name {  get; }

    public WorldObject(Vector2 position, string name)
    {
        if (position == null)
            throw new NullReferenceException("position");

        if(String.IsNullOrEmpty(name))
            throw new ArgumentNullException("name");

        Position = new Vector2(position.x, position.y);
        Name = name;
    }

    public abstract void Update(float deltaTime);
}
