using static System.Console;

////////////
// ДАННЫЕ //
////////////

//id локаций
const int LOCATION_DOOR = 1;
const int LOCATION_TABLE = 2;
const int LOCATION_PICTURE = 3;

//состояние картины
const int PICTURE_ON_WALL = 1;
const int PICTURE_SAFE_LOCKED = 2;
const int PICTURE_SAFE_UNLOCKED = 3;

//данные
int  picture_state = PICTURE_ON_WALL;
int  code = Random.Shared.Next(100,1000);
bool door_locked = true;
bool has_key = false;
bool end = false;

/////////////
// ИСТОРИЯ //
/////////////
var story = new StoryBuilder("Вы заперты. Надо что-то делать.", "Поздравляем, вы выбрались!", LOCATION_DOOR)

    .AddLocation(LOCATION_DOOR,    GetDoorDescription)
    .AddLocation(LOCATION_TABLE,   "Вы стоите рядом с низеньким журнальным столиком. На нем лежит потрепанная газета.")
    .AddLocation(LOCATION_PICTURE, GetPictureDescription)

    .AddOption(LOCATION_DOOR, LOCATION_PICTURE, "перейти к картине")
    .AddOption(LOCATION_DOOR, LOCATION_TABLE,   "перейти к столику")
    .AddOption(LOCATION_DOOR, "отпереть дверь", DoUnlockDoor, () => door_locked)
    .AddOption(LOCATION_DOOR, "уйти",           DoLeave,      () => !door_locked)

    .AddOption(LOCATION_PICTURE, LOCATION_DOOR,  "перейти к двери")
    .AddOption(LOCATION_PICTURE, LOCATION_TABLE, "перейти к столику")
    .AddOption(LOCATION_PICTURE, "поправить картину", DoRepairPicture, () => picture_state == PICTURE_ON_WALL)
    .AddOption(LOCATION_PICTURE, "ввести код",        DoEnterCode,     () => picture_state == PICTURE_SAFE_LOCKED)
    .AddOption(LOCATION_PICTURE, "взять ключ",        DoGetKey,        () => picture_state == PICTURE_SAFE_UNLOCKED && !has_key)

    .AddOption(LOCATION_TABLE, LOCATION_DOOR,    "перейти к двери")
    .AddOption(LOCATION_TABLE, LOCATION_PICTURE, "перейти к картине")
    .AddOption(LOCATION_TABLE, "прочитать газету", DoReadNewspaper)

    .Build();

////////////
// движок //
////////////

Clear();
WriteLine(story.Inro);
while (!end)
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

////////////
// прочее //
////////////

string GetDoorDescription()
{
    string desc = "Перед вами толстая дубовая дверь";
    if (door_locked)
        desc += "\nОна заперта на тяжелый навесной замок";
    return desc;
}

string GetPictureDescription()
{
    string desc = "На стене висит грязная картина. Криво висит.";
    if (picture_state == PICTURE_SAFE_LOCKED)
        desc = "Под ногами валяется картина а на ее месте - встроенный сейф с кодовым замком.";
    if (picture_state == PICTURE_SAFE_UNLOCKED)
    {
        desc = "Под ногами валяется картина а на ее месте - распахнутый сейф.";
        if (!has_key)
            desc += "\nВ глубине сейфа что-то блестит...";
        else
            desc += "\nСейф абсолютно пуст.";
    }
    return desc;
}

void DoUnlockDoor()
{
    if (has_key)
    {
        door_locked = false;
        Alert("Вы отпираете дверь найденным ключом!");
    }
    else
    {
        Alert("Для этого нужен ключ...");
    }
}

void DoLeave ()
{
    end = true;
    Alert("Вы открываете дверь и успешно покидаете это проклятую комнату!");
}

void DoReadNewspaper()
{
    Alert("Вы берете в руки газету, но смогли разобрать только:");
    Alert($"...хищено {code} миллио...");
}

void DoRepairPicture ()
{
    Alert("Вы поправляете картину, но внезапно она срывается, а за ней...");
    Alert("Обнаруживается скрытый сейф!");
    picture_state = PICTURE_SAFE_LOCKED;
}

void DoEnterCode ()
{
    var c = GetInt("Введите код (3 цифры): ", 100, 999);
    if (c != code)
        Alert("Клик! Не подходит...");
    else
    {
        Alert("Клик! Похоже код подошел. Дверца сейфа медленно открывается...");
        picture_state = PICTURE_SAFE_UNLOCKED;
    }
}

void DoGetKey ()
{
    Alert("А вот и тот ключ, который вы так долго искали!");
    has_key = true;
}

void Alert (string msg)
{
    var old = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkRed;

    WriteLine(msg);

    ForegroundColor = old;
}

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