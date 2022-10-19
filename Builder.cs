class StoryBuilder
{
    private Story story;

    public StoryBuilder(string intro = "", string final = "")
    {
        story = new Story();
        story.Inro = intro;
        story.Final = final;
    }

    public StoryBuilder SetCurrentlocation(int id)
    {
        story.currentId = id;
        return this;
    }

    public StoryBuilder AddLocation(int locationId, string description)
    {
        story.Locations.Add
        (
            new Location()
            {
                Id = locationId,
                Description = () => description,
                Options = new List<Option>()
            }
        );
        return this;
    }

    public StoryBuilder AddLocation(int locationId, DelegateForMessage description)
    {
        story.Locations.Add
        (
            new Location()
            {
                Id = locationId,
                Description = description,
                Options = new List<Option>()
            }
        );
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, int toLocationId, string title, string message = "")
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Message = message,
            Visible = () => true,
            Work = () => story.currentId = toLocationId
        };
        location.Options.Add(action);
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, int toLocationId, string title, string message, DelegateForVisible visible)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Message = message,
            Visible = visible,
            Work = () => story.currentId = toLocationId
        };
        location.Options.Add(action);
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, string title, string message, DelegateForWork work)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Message = message,
            Visible = () => true,
            Work = work
        };
        location.Options.Add(action);
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, string title, string message, DelegateForWork work, DelegateForVisible visible)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Message = message,
            Visible = visible,
            Work = work
        };
        location.Options.Add(action);
        return this;
    }

    public Story Build()
    {
        return story;
    }
}