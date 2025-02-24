using System;

class Player
{
    private Room currentRoom;

    public Room SetCurrentRoom(Room selectedRoom) {
        currentRoom = selectedRoom;
        return currentRoom;
    }

    public Room GetCurrentRoom() {
        return currentRoom;
    }
}