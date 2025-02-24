using System;

class Player
{
    private Room currentRoom;

    public Room CurrentRoom {
        get {
            return this.currentRoom;
        }
        set {
            this.currentRoom = value;
        }
    }
}