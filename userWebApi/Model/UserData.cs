using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace userWebApi.Model
{
    public class UserData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsMale { get; set; }
        public string Education { get; set; }
        public bool HasCar { get; set; }

        public RegistrationMessage GetRegisteredMessage()
        {
            var sexData = IsMale ? "Мужской" : "Женский";

            var messages = new List<string>()
            {
                "Cпасибо за регистрацию.",
                $"Ваш логин: {Name} ",
                $"Ваш пароль: {Password}",
                "Вы указали следующие данные:",
                $"Образование: {MakeEducationString(Education)}", 
                $"пол: {sexData}",
                HasCar ? "Имеется автомобиль" : "Автомобиль отсутствует"
            };

            return
                new RegistrationMessage
                {
                    Messages = messages
                };
        }

        private static string MakeEducationString(string education)
        {
            switch (education)
            {
                case "High":
                    return "Высшее";
                case "Partly":
                    return "Незаконченное высшее";
                case "Middle":
                    return "Среднее";
                default:
                    return "Загадочное";
            }
        }
    }
}
