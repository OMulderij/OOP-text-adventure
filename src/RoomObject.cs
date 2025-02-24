using System;

class RoomObject
{
    private string objectName;
    private string objectDescription;
    private string objectEffect;

    public RoomObject(string name) {
        objectName = name;
    }

    public string ObjectName {
        get {
            return this.objectName;
        }
    }

    public string ObjectDescription {
        set {
            this.objectDescription = value;
        }
        get {
            if (objectDescription == null) {
                return "This item does not have a description.";
            }
            return this.objectDescription;
        }
    }

    public string ObjectEffect {
        set {
            this.objectDescription = value;
        }
        get {
            if (this.objectEffect == null) {
                return "This item does not any effects.";
            }
            return this.objectEffect;
        }
    }
}