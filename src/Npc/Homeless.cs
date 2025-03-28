class Homeless : Npc
{
    public Homeless() : base("hobo") {}

    public override string Talk()
    {
        string str = "";
        str += "I see you are interested in the secrets of this world...\n";
        str += "You have come to the right place my friend.\n";
        str += "Because I, THE GIVER OF TRUTH have appeared before you.\n";
        str += "Now you might be wondering who exactly I am.\n";
        str += "But the time has not yet come.\n";
        str += "Something else you might wonder, is what I have to offer.\n";
        str += "And that, is a good question.\n";
        str += "Don't you think?\n";
        str += "I'm glad we think alike, it's much easier to communicate that way.\n";
        
        str += "You've already listened to me rambling for a bit, but I just realised I don't even know your name.\n";
        str += "The silent type huh?\n";
        str += "That's okay, I'll just call you 'Quiet' then.\n";
        str += "A fitting name right?\n";
        str += "I almost feel like you're my dog, or something like that.\n";
        str += "Who am I kidding, no sane person would bring their dog to a hellhole like Night City.\n";
        str += "They need a safe haven to thrive.\n";
        str += "I wish all dogs had a safe haven to call home...\n";
        
        str += "You must really like this place.\n";
        str += "Would you mind sharing your secrets of pleasure to me?\n";
        str += "Still nothing?\n";
        str += "You know, I've been trying to find happiness in here for a long time.\n";
        str += "But every time I try to do anything, it crumbles right down to where I started.\n";
        str += "Like, last time, I tried starting a business.\n";
        str += "My plan was to create some bigger than anything I've made before.\n";
        str += "What kind of business, you wonder?\n";
        str += "It was a business to tickle a niche nobody has ever touched before.\n";
        str += "A service to decorate millitary equipment with the most amazing decorations ever.\n";
        str += "Is that doubt I see in your eyes?\n";
        str += "Doubt not, for the business was very successful for a total of 4 days!\n";
        str += "A pretty good record, is it not? It's the best I've managed so far.\n";
        str += "Thank you, thank you. Praise is not neccesary, don't worry.\n";
        
        str += "Yesterday, some gonks-ass corpos roamed the street.\n";
        str += "And guess what? those A-holes ransacked my entire place.\n";
        str += "I've mostly managed to clean the mess up, but some of my valuables were lost in the process.\n";
        str += "I was super lucky to not have been there, just imagine what they wouldve done to me!\n";

        return str;
    }
}