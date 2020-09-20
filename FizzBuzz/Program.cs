using System;

namespace FizzBuzz
{
    class Tag
    {
        public string _value { get; set; }
        public Tag(string value)
        {
            _value = value;
        }
    }

    class Printer
    {
        public Tag _context { get; set; }
        public Printer(Tag context)
        {
            _context = context;
        }
        public void print()
        {
            Console.WriteLine(_context._value);
        }
    }

    class DivCondition
    {
        private readonly int _divider;
        public DivCondition(int divider)
        {
            _divider = divider;
        }
        public bool isTruthy(int num)
        {
            return num % _divider == 0;
        }
    }
    class AndStrategy
    {
        private DivCondition[] conditions;
        public AndStrategy(DivCondition[] conditionsOrStrategies)
        {
            conditions = conditionsOrStrategies;
        }
        public bool isTruthy(int num)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].isTruthy(num))
                {
                    return false;
                }
            }
            return true;
        }
    }
    class TagNumRule
    {
        public readonly Tag _tag;
        private readonly AndStrategy _strategy;

        public TagNumRule(Tag tag, AndStrategy strategy)
        {
            this._tag = tag;
            this._strategy = strategy;
        }

        public bool isSuccess(int num)
        {
            return this._strategy.isTruthy(num);
        }
    }

    class TagNumRulesCollection
    {
        private readonly TagNumRule[] _tags;

        public TagNumRulesCollection(TagNumRule[] tags)
        {
            this._tags = tags;
        }
        public Tag find(int num, Tag defaultValue)
        {
            for (int i = 0; i < _tags.Length; i++)
            {
                if (this._tags[i].isSuccess(num))
                {
                    return this._tags[i]._tag;
                }
            }
            return defaultValue;
        }
    }
    class Program
    {
        private const int MAX_NUM = 100;


        static void Main(string[] args)
        {
            TagNumRulesCollection numTags = new TagNumRulesCollection(new TagNumRule[]
            {
                new TagNumRule(new Tag("FizzBuzz"), new AndStrategy(new DivCondition[] { new DivCondition(3), new DivCondition(5)})),
                new TagNumRule(new Tag("Fizz"), new AndStrategy(new DivCondition[] { new DivCondition(3) })),
                new TagNumRule(new Tag("Buzz"), new AndStrategy(new DivCondition[] { new DivCondition(5) })),
            });

            for (int i = 1; i < MAX_NUM; i++)
            {
                new Printer(numTags.find(i, new Tag(i.ToString()))).print();
            }
        }
    }
}
