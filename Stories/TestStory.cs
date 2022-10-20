using static System.Console;
using static Tools;

class TestStory
{
    const int LOCATION_LEFT = 1;
    const int LOCATION_RIGHT = 2;

    public Story Create()
    {
        return new StoryBuilder("Вы крепко спите", "Как вы сюда попали?!", LOCATION_LEFT)
            .AddLocation(LOCATION_LEFT, "Вы спите на левом боку")
            .AddLocation(LOCATION_RIGHT, "Вы спите на правом боку")
            
            .AddOption(LOCATION_LEFT, LOCATION_RIGHT, "Перевернуться на правый бок")
            .AddOption(LOCATION_LEFT, "Встать", () => Alert("Нафигаааа?!"))

            .AddOption(LOCATION_RIGHT, LOCATION_LEFT, "Перевернуться на левый бок")
            .AddOption(LOCATION_RIGHT, "Проснуться", () => Alert("Зачеееем?!"))

            .Build();
    }

}