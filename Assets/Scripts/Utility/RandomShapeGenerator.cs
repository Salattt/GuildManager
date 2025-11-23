using UnityEngine;

public static class RandomShapeGenerator
{
    public static TurnebleShape GenerateRandomShape(int level, float angle, Vector2 position, float scaleToMap)
    {
        switch(Random.Range(0,3))
        {
            case 0:
                return new Square(level,angle,position,scaleToMap);

            case 1:
                return new Triangle(level,angle,position, scaleToMap);

            case 2:
                return new Circle(level,angle,position,scaleToMap);
        }

        return new Square(level, angle, position, scaleToMap);
    }
}
