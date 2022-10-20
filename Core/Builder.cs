class StoryBuilder
{
    private Story story;

    public StoryBuilder(string intro = "", string final = "", int startid = 0)
    {
        story = new Story()
        {
            End = false,
            Inro = intro,
            Final = final,
            currentId = startid
        };
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

    public StoryBuilder AddOption(int forLocationId, int toLocationId, string title)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Visible = () => true,
            Work = () => story.currentId = toLocationId
        };
        location.Options.Add(action);
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, int toLocationId, string title, DelegateForVisible visible)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Visible = visible,
            Work = () => story.currentId = toLocationId
        };
        location.Options.Add(action);
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, string title, DelegateForWork work)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
            Visible = () => true,
            Work = work
        };
        location.Options.Add(action);
        return this;
    }

    public StoryBuilder AddOption(int forLocationId, string title, DelegateForWork work, DelegateForVisible visible)
    {
        var location = story.Locations.First(l => l.Id == forLocationId);
        var action = new Option()
        {
            Title = title,
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