namespace Pravoo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Основной класс программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Словарь похожих по начертанию на русские буквы английских букв
        /// </summary>
        public static Dictionary<char, char> latinToCyrillicDictionary = new Dictionary<char, char>
        {
            ['a'] = 'а',
            ['e'] = 'е',
            ['o'] = 'о',
            ['p'] = 'р',
            ['c'] = 'с',
            ['y'] = 'у',
            ['x'] = 'х',
            ['u'] = 'и'
        };
        /// <summary>
        /// Словарь похожих по начертанию на русские буквы цифр
        /// </summary>
        public static Dictionary<char, char> digitToCyrillicDictionary = new Dictionary<char, char>
        {
            ['3'] = 'з',
            ['0'] = 'о',
            ['4'] = 'ч',
            ['6'] = 'б',
            ['8'] = 'в'
        };
        /// <summary>
        /// Переменная для хранения считываемого текста
        /// </summary>
        public static string text = "";

        /// <summary>
        /// Главная функция программы
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        private static void Main(string[] args)
        {
            //Создание диалога выбора файла для открытия
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                //Если нажата кнопка "Отменить"
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    //Чтение текста из файла
                    using (StreamReader reader = new StreamReader(dialog.FileName))
                    {
                        //Проверка кодировки
                        if (!ReferenceEquals(reader.CurrentEncoding, Encoding.UTF8))
                        {
                            Console.WriteLine("Неверная кодировка текста в файле! Необходима кодировка UTF-8.");
                            Console.ReadKey();
                        }
                        else
                        {
                            //Чтение текста
                            text = reader.ReadToEnd();
                            //Задание 1
                            text = textNormalizer(text);
                            //Задание 2
                            text = Replacer(text);
                        }
                    }
                }
            }
            //Создание диалога выбора пути сохранения
            using (SaveFileDialog dialog2 = new SaveFileDialog())
            {
                dialog2.Filter = "txt files (*.txt)|*.txt";
                dialog2.FilterIndex = 2;
                dialog2.RestoreDirectory = true;
                if (dialog2.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(dialog2.FileName))
                    {
                        writer.WriteLine(text);
                    }
                }
            }
        }

        /// <summary>
        /// Функция замены похожих по начертанию на русские буквы английских букв и цифр в тексте
        /// </summary>
        /// <param name="text">Текст для замены</param>
        /// <returns>Текст с выполненной заменой</returns>
        public static string Replacer(string text)
        {
            //Переменная для хранения нового текста
            string newText = "";
            //Строки текста
            List<string> lines = new List<string>();
            //Слова в строке
            List<string> words = new List<string>();
            bool flag = true;
            //Разбитие текста на строки
            lines = text.Split('\n').ToList();
            for(int i = 0; i < lines.Count; i++)
            {
                //Разбитие строки на слова
                words = lines[i].Split(' ').ToList();
                lines[i] = "";
                for(int j = 0; j < words.Count; j++)
                {
                    //Проверка на наличие английских букв
                    foreach (var key in latinToCyrillicDictionary.Keys)
                    {
                        if (words[j].Contains(key))
                        {
                            flag = true;
                            break;
                        }
                    }
                    //Проверка на наличие цифр
                    foreach (var key in digitToCyrillicDictionary.Keys)
                    {
                        if (words[j].Contains(key))
                        {
                            flag = true;
                            break;
                        }
                    }

                    //Если есть английские буквы или цифры
                    if(flag)
                    {
                        //Формирование новой строки
                        if (j == words.Count - 1)
                        {
                            lines[i] += rusReplace(words[j]);
                        }
                        else
                        {
                            lines[i] += rusReplace(words[j]) + " ";
                        }
                    }
                }
                //Формирование нового текста
                if (i == lines.Count - 1)
                {
                    newText += rusReplace(lines[i]);
                }
                else
                {
                    newText += rusReplace(lines[i] + "\n");
                }
            }
            return newText;
        }

        /// <summary>
        /// Функция замены похожих по начертанию на русские буквы английских букв и цифр в слове
        /// </summary>
        /// <param name="word">Слово для замены</param>
        /// <returns>Замененное слово</returns>
        public static string rusReplace(string word)
        {
            string str = word;
            for (int i = 0; i < word.Length; i++)
            {
                //Если буква
                if (latinToCyrillicDictionary.ContainsKey(str[i]))
                {
                    str = str.Replace(str[i], latinToCyrillicDictionary[str[i]]);
                }
                //Если цифра
                if (digitToCyrillicDictionary.ContainsKey(str[i]))
                {
                    str = str.Replace(str[i], digitToCyrillicDictionary[str[i]]);
                }
            }
            return str;
        }

        /// <summary>
        /// Функция удаления символов и приведения текста к нижнему регистру
        /// </summary>
        /// <param name="text">Текст для преобразования</param>
        /// <returns>Преобразованный текст</returns>
        public static string textNormalizer(string text)
        {
            return new string(text.Where(c => ((c == '\0') || ((c == ' ') || (c == '\n'))) || char.IsLetterOrDigit(c)).ToArray()).ToLower();
        }
    }
}