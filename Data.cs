class Story
{
    public int currentId;
    public string Inro;
    public string Final;
    public List<Location> Locations = new List<Location>();

    public Location CurrentLocation { get => Locations.First(loc => loc.Id == currentId); }
}

class Location
{
    public int Id;
    public DelegateForMessage Description;
    public List<Option> Options = new List<Option>();
}

class Option
{
    public string Title;
    public DelegateForVisible Visible;
    public DelegateForWork Work;
}

delegate bool DelegateForVisible ();
delegate string DelegateForMessage ();
delegate void DelegateForWork ();