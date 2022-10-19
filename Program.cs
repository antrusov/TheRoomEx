using static System.Console;

////////////
// ДАННЫЕ //
////////////

//id локаций
const int LOCATION_DOOR = 1;
const int LOCATION_TABLE = 2;
const int LOCATION_PICTURE = 3;
const int LOCATION_END = 4;

/////////////
// ИСТОРИЯ //
/////////////
var story = new StoryBuilder("Вы заперты. Надо что-то делать.", "Поздравляем, вы выбрались!")

    .AddLocation(LOCATION_DOOR,    "Перед вами дверь, запертая на тяжелый навесной замок")
    .AddLocation(LOCATION_TABLE,   "Вы стоите рядом с низеньким журнальным столиком. На нем лежит потрепанная газета.")
    .AddLocation(LOCATION_PICTURE, "Вы видите картину, которая висит на стене.")
    .AddLocation(LOCATION_END,     "За дверью открывается чудесный вид на лазурное море.")

    .AddOption(LOCATION_DOOR,    LOCATION_PICTURE, "перейти к картине")
    .AddOption(LOCATION_DOOR,    LOCATION_TABLE,   "перейти к столику")
    .AddOption(LOCATION_DOOR,    LOCATION_END,     "уйти")
    .AddOption(LOCATION_PICTURE, LOCATION_DOOR,    "перейти к двери")
    .AddOption(LOCATION_PICTURE, LOCATION_TABLE,   "перейти к столику")
    .AddOption(LOCATION_TABLE,   LOCATION_DOOR,    "перейти к двери")
    .AddOption(LOCATION_TABLE,   LOCATION_PICTURE, "перейти к картине")

    .SetCurrentlocation(LOCATION_DOOR)

    .Build();

////////////
// движок //
////////////

Clear();
WriteLine(story.Inro);
while (true)
{
    var loc = story.CurrentLocation;

    //описание локации
    WriteLine();
    WriteLine(loc.Description());

    //выход, если нет вариантов
    if (loc.Options.Count <= 0)
        break;

    //отфильтрованный список пунктов
    var opts = loc.Options.Where(loc => loc.Visible()).ToList();

    for(int i=0; i<opts.Count; i++)
        WriteLine($"{i+1}) {opts[i].Title}");

    //выбор
    int n = GetInt("Ваш выбор: ", 1, opts.Count) - 1;

    //выполнение действия
    var opt = opts[n];
    if (!string.IsNullOrWhiteSpace(opt.Message))
        WriteLine(opt.Message);
    opt.Work();
}
WriteLine(story.Final);
ReadKey();

////////////
// прочее //
////////////

int GetInt (string message, int min, int max)
{
    int result = min;
    bool valid = false;
    do
    {
        Write(message);
        valid = int.TryParse(ReadLine(), out result);
    } while (!valid || result < min || result > max);
    return result;
}