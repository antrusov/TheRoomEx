using static System.Console;
using static Tools;

//var story = new TestStory().Create();
var story = new RoomStory().Create();

Clear();
WriteLine(story.Inro);
while (!story.End)
{
    var loc = story.CurrentLocation;

    //описание локации
    WriteLine();
    WriteLine(loc.Description());

    //отфильтрованный список пунктов
    var opts = loc.Options.Where(loc => loc.Visible()).ToList();
    if (opts.Count <= 0) break;

    //вывод списка пунктов
    for(int i=0; i<opts.Count; i++)
        WriteLine($"{i+1}) {opts[i].Title}");

    //выбор
    int n = GetInt("Ваш выбор: ", 1, opts.Count) - 1;

    //выполнение действия
    opts[n].Work();
}
WriteLine(story.Final);
ReadKey();