using static System.Console;
using static Tools;

class RoomStory
{
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
    Story story = null;

    public Story Create()
    {
         story = new StoryBuilder("Вы заперты. Надо что-то делать.", "Поздравляем, вы выбрались!", LOCATION_DOOR)

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

        return story;
    }

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
        story.End = true;
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

}
