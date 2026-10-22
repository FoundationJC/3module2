using System;

namespace ClassLibrary3module2
{
    public class Strings : IComparable<Strings>
    {
        // Дані з варіанту
        public string Value { get; set; }        // значення рядка
        public int Length { get; private set; }  // довжина (автоматично оновлюється)

        // Закритий ключ шифрування: різниця кодів та напрямок
        private int _shift;        // на скільки символів зсувати (наприклад, 3)
        private bool _shiftRight;  // напрямок: true = вправо (+), false = вліво (-)

        // Конструктор
        public Strings(string value, int shift, bool shiftRight)
        {
            Value = value;
            _shift = shift;
            _shiftRight = shiftRight;
            UpdateLength();
        }

        private void UpdateLength() => Length = Value?.Length ?? 0;

        // Метод 1: Шифрування (заміна значущих символів на символи з кодом більшим/меншим)
        public void Encrypt()
        {
            char[] chars = Value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i])) // шифруємо лише літери
                {
                    int offset = _shiftRight ? _shift : -_shift;
                    chars[i] = (char)(chars[i] + offset);
                }
            }
            Value = new string(chars);
            UpdateLength();
        }

        // Метод 2: Дешифрування (зворотна операція)
        public void Decrypt()
        {
            char[] chars = Value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i]))
                {
                    int offset = _shiftRight ? -_shift : _shift;
                    chars[i] = (char)(chars[i] + offset);
                }
            }
            Value = new string(chars);
            UpdateLength();
        }

        // Метод 3: Виведення інформації
        public void Print()
        {
            Console.WriteLine($"Рядок: \"{Value}\", Довжина: {Length}");
        }

        public override string ToString() => $"\"{Value}\" (довжина {Length})";

        // Реалізація IComparable<CryptoString> для сортування за довжиною
        public int CompareTo(Strings? other)
        {
            if (other == null) return 1;
            return this.Length.CompareTo(other.Length); // порівняння за довжиною
        }
    }
}
