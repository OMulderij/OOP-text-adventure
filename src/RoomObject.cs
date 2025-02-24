using System;

class RoomObject
{
    private string objectName;
    private string objectDescription;
    private string objectEffect;

    public RoomObject(string name) {
        objectName = name;
    }

    public void AddObjectDescription(string newObjectDescription) {
        objectDescription = newObjectDescription;
    }

    public void AddObjectEffect(string newObjectEffect) {
        objectDescription = newObjectEffect;
    }

    public string GetObjectName() {
        return objectName;
    }

    public string GetObjectDescription() {
        if (objectDescription == null) {
            return "This item does not have a description.";
        }
        return objectDescription;
    }

    public string GetObjectEffect() {
        if (objectEffect == null) {
            return "This item does not any effects.";
        }
        return objectEffect;
    }
}