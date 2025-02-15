using System;

class RoomObject
{
    private string objectName;
    private string objectDescription;
    private string objectEffect;

    public RoomObject(string name) {
        objectName = name;
    }

    public string AddObjectDescription(string newObjectDescription) {
        objectDescription = newObjectDescription;
        return objectDescription;
    }

    public string AddObjectEffect(string newObjectEffect) {
        objectDescription = newObjectEffect;
        return objectDescription;
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