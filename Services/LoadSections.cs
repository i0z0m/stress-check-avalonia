using StressCheckAvalonia.Models;
using System.Collections.Generic;

namespace StressCheckAvalonia.Services
{
    public static class LoadSections
    {
        public static List<Section> sections = new List<Section>
    {
        new Section
        {
            Step = 1,
            Name = "仕事について",
            Description = "あなたの仕事についてうかがいます。4つの中から最もあてはまる選択ボタンを選んでください。（全17問）",
            Questions = new List<Question>
            {
                new Question { Id = 1, Text = "非常にたくさんの仕事をしなければならない", Score = 0, Reverse = true },
                new Question { Id = 2, Text = "時間内に仕事が処理しきれない", Score = 0, Reverse = true },
                new Question { Id = 3, Text = "一生懸命働かなければならない", Score = 0, Reverse = true },
                new Question { Id = 4, Text = "かなり注意を集中する必要がある", Score = 0, Reverse = true },
                new Question { Id = 5, Text = "高度の知識や技術が必要なむずかしい仕事だ", Score = 0, Reverse = true },
                new Question { Id = 6, Text = "勤務時間中はいつも仕事のことを考えていなければならない", Score = 0, Reverse = true },
                new Question { Id = 7, Text = "からだを大変よく使う仕事だ", Score = 0, Reverse = true },
                new Question { Id = 8, Text = "自分のペースで仕事ができる", Score = 0, Reverse = false },
                new Question { Id = 9, Text = "自分で仕事の順番・やり方を決めることができる", Score = 0, Reverse = false },
                new Question { Id = 10, Text = "職場の仕事の方針に自分の意見を反映できる", Score = 0, Reverse = false },
                new Question { Id = 11, Text = "自分の技能や知識を仕事で使うことが少ない", Score = 0, Reverse = true },
                new Question { Id = 12, Text = "私の部署内で意見のくい違いがある", Score = 0, Reverse = true },
                new Question { Id = 13, Text = "私の部署と他の部署とはうまが合わない", Score = 0, Reverse = true },
                new Question { Id = 14, Text = "私の職場の雰囲気は友好的である", Score = 0, Reverse = false },
                new Question { Id = 15, Text = "私の職場の作業環境（騒音、照明、温度、換気など）はよくない", Score = 0, Reverse = true },
                new Question { Id = 16, Text = "仕事の内容は自分にあっている", Score = 0, Reverse = false },
                new Question { Id = 17, Text = "働きがいのある仕事だ", Score = 0, Reverse = false },
            },
            Choices = new List<string> { "そうだ", "まあそうだ", "ややちがう", "ちがう" },
            Group = "ストレスの原因因子",
            Factors = new List<Factor>
            {
                new Factor
                {
                    Point = 1,
                    Scale = "心理的な\n仕事の負担（量）",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 5, Value = 5 },
                        new Rate { Min = 6, Max = 7, Value = 4 },
                        new Rate { Min = 8, Max = 9, Value = 3 },
                        new Rate { Min = 10, Max = 11, Value = 2 },
                        new Rate { Min = 12, Max = 12, Value = 1 },
                    },
                    Items = new List<int> { 1, 2, 3 },
                },
                new Factor
                {
                    Point = 2,
                    Scale = "心理的な\n仕事の負担（質）",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 5, Value = 5 },
                        new Rate { Min = 6, Max = 7, Value = 4 },
                        new Rate { Min = 8, Max = 9, Value = 3 },
                        new Rate { Min = 10, Max = 11, Value = 2 },
                        new Rate { Min = 12, Max = 12, Value = 1 },
                    },
                    Items = new List<int> { 4, 5, 6 },
                },
                new Factor
                {
                    Point = 3,
                    Scale = "自覚的な\n身体的負担度",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 1, Max = 1, Value = 4 },
                        new Rate { Min = 2, Max = 2, Value = 3 },
                        new Rate { Min = 3, Max = 3, Value = 2 },
                        new Rate { Min = 4, Max = 4, Value = 1 },
                    },
                    Items = new List<int> { 7 },
                },
                new Factor
                {
                    Point = 4,
                    Scale = "職場の対人\n関係でのストレス",
                    Value = 0,
                    Type = "complex",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 1, Max = 2, Value = 6 },
                        new Rate { Min = 3, Max = 3, Value = 5 },
                        new Rate { Min = 4, Max = 5, Value = 4 },
                        new Rate { Min = 6, Max = 7, Value = 3 },
                        new Rate { Min = 8, Max = 9, Value = 2 },
                        new Rate { Min = 10, Max = 12, Value = 1 },
                    },
                    Items = new List<int> { 12, 13, 14 },
                },
                new Factor
                {
                    Point = 5,
                    Scale = "職場環境による\nストレス",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 1, Max = 1, Value = 4 },
                        new Rate { Min = 2, Max = 2, Value = 3 },
                        new Rate { Min = 3, Max = 3, Value = 2 },
                        new Rate { Min = 4, Max = 4, Value = 1 },
                    },
                    Items = new List<int> { 15 },
                },
                new Factor
                {
                    Point = 6,
                    Scale = "仕事の\nコントロール度",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 4, Value = 1 },
                        new Rate { Min = 5, Max = 6, Value = 2 },
                        new Rate { Min = 7, Max = 8, Value = 3 },
                        new Rate { Min = 9, Max = 10, Value = 4 },
                        new Rate { Min = 11, Max = 12, Value = 5 },
                    },
                    Items = new List<int> { 8, 9, 10 },
                },
                new Factor
                {
                    Point = 7,
                    Scale = "あなたの\n技能の活用度",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 1, Max = 1, Value = 1 },
                        new Rate { Min = 2, Max = 2, Value = 2 },
                        new Rate { Min = 3, Max = 3, Value = 3 },
                        new Rate { Min = 4, Max = 4, Value = 4 },
                    },
                    Items = new List<int> { 11 },
                },
                new Factor
                {
                    Point = 8,
                    Scale = "あなたが\n感じている\n仕事の適性度",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 1, Max = 1, Value = 1 },
                        new Rate { Min = 2, Max = 2, Value = 2 },
                        new Rate { Min = 3, Max = 3, Value = 3 },
                        new Rate { Min = 4, Max = 4, Value = 5 },
                    },
                    Items = new List<int> { 16 },
                },
                new Factor
                {
                    Point = 9,
                    Scale = "働きがい",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 1, Max = 1, Value = 1 },
                        new Rate { Min = 2, Max = 2, Value = 2 },
                        new Rate { Min = 3, Max = 3, Value = 3 },
                        new Rate { Min = 4, Max = 4, Value = 5 },
                    },
                    Items = new List<int> { 17 },
                },
            }
        },
        new Section
        {
            Step = 2,
            Name = "最近1か月の状態について",
            Description = "最近 1 か月間のあなたの状態についてうかがいます。4つの中から最もあてはまる選択ボタンを選んでください。（全29問）",
            Questions = new List<Question>
            {
                new Question { Id = 1, Text = "活気がわいてくる", Score = 0, Reverse = true },
                new Question { Id = 2, Text = "元気がいっぱいだ", Score = 0, Reverse = true },
                new Question { Id = 3, Text = "生き生きする", Score = 0, Reverse = true },
                new Question { Id = 4, Text = "怒りを感じる", Score = 0, Reverse = false },
                new Question { Id = 5, Text = "内心腹立たしい", Score = 0, Reverse = false },
                new Question { Id = 6, Text = "イライラしている", Score = 0, Reverse = false },
                new Question { Id = 7, Text = "ひどく疲れた", Score = 0, Reverse = false },
                new Question { Id = 8, Text = "へとへとだ", Score = 0, Reverse = false },
                new Question { Id = 9, Text = "だるい", Score = 0, Reverse = false },
                new Question { Id = 10, Text = "気がはりつめている", Score = 0, Reverse = false },
                new Question { Id = 11, Text = "不安だ", Score = 0, Reverse = false },
                new Question { Id = 12, Text = "落着かない", Score = 0, Reverse = false },
                new Question { Id = 13, Text = "ゆううつだ", Score = 0, Reverse = false },
                new Question { Id = 14, Text = "何をするのも面倒だ", Score = 0, Reverse = false },
                new Question { Id = 15, Text = "物事に集中できない", Score = 0, Reverse = false },
                new Question { Id = 16, Text = "気分が晴れない", Score = 0, Reverse = false },
                new Question { Id = 17, Text = "仕事が手につかない", Score = 0, Reverse = false },
                new Question { Id = 18, Text = "悲しいと感じる", Score = 0, Reverse = false },
                new Question { Id = 19, Text = "めまいがする", Score = 0, Reverse = false },
                new Question { Id = 20, Text = "体のふしぶしが痛む", Score = 0, Reverse = false },
                new Question { Id = 21, Text = "頭が重かったり頭痛がする", Score = 0, Reverse = false },
                new Question { Id = 22, Text = "首筋や肩がこる", Score = 0, Reverse = false },
                new Question { Id = 23, Text = "腰が痛い", Score = 0, Reverse = false },
                new Question { Id = 24, Text = "目が疲れる", Score = 0, Reverse = false },
                new Question { Id = 25, Text = "動悸や息切れがする", Score = 0, Reverse = false },
                new Question { Id = 26, Text = "胃腸の具合が悪い", Score = 0, Reverse = false },
                new Question { Id = 27, Text = "食欲がない", Score = 0, Reverse = false },
                new Question { Id = 28, Text = "便秘や下痢をする", Score = 0, Reverse = false },
                new Question { Id = 29, Text = "よく眠れない", Score = 0, Reverse = false },
            },
                Choices = new List<string> { "ほとんどなかった", "ときどきあった", "しばしばあった", "ほとんどいつもあった" },
                Group = "ストレスによる心身反応",
                Factors = new List<Factor>
            {
                new Factor
                {
                    Point = 1,
                    Scale = "活気",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 3, Value = 1 },
                        new Rate { Min = 4, Max = 5, Value = 2 },
                        new Rate { Min = 6, Max = 7, Value = 3 },
                        new Rate { Min = 8, Max = 9, Value = 4 },
                        new Rate { Min = 10, Max = 12, Value = 5 },
                    },
                    Items = new List<int> { 1, 2, 3 },
                },
                new Factor
                {
                    Point = 2,
                    Scale = "イライラ感",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 3, Value = 5 },
                        new Rate { Min = 4, Max = 5, Value = 4 },
                        new Rate { Min = 6, Max = 7, Value = 3 },
                        new Rate { Min = 8, Max = 9, Value = 2 },
                        new Rate { Min = 10, Max = 12, Value = 1 },
                    },
                    Items = new List<int> { 4, 5, 6 },
                },
                new Factor
                {
                    Point = 3,
                    Scale = "疲労感",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 3, Value = 5 },
                        new Rate { Min = 4, Max = 4, Value = 4 },
                        new Rate { Min = 5, Max = 7, Value = 3 },
                        new Rate { Min = 8, Max = 10, Value = 2 },
                        new Rate { Min = 11, Max = 12, Value = 1 },
                    },
                    Items = new List<int> { 7, 8, 9 },
                },
                new Factor
                {
                    Point = 4,
                    Scale = "不安感",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 3, Value = 5 },
                        new Rate { Min = 4, Max = 4, Value = 4 },
                        new Rate { Min = 5, Max = 7, Value = 3 },
                        new Rate { Min = 8, Max = 9, Value = 2 },
                        new Rate { Min = 10, Max = 12, Value = 1 },
                    },
                    Items = new List<int> { 10, 11, 12 },
                },
                new Factor
                {
                    Point = 5,
                    Scale = "抑うつ感",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 6, Max = 6, Value = 5 },
                        new Rate { Min = 7, Max = 8, Value = 4 },
                        new Rate { Min = 9, Max = 12, Value = 3 },
                        new Rate { Min = 13, Max = 16, Value = 2 },
                        new Rate { Min = 17, Max = 24, Value = 1 },
                    },
                    Items = new List<int> { 13, 14, 15, 16, 17, 18 },
                },
                new Factor
                {
                    Point = 6,
                    Scale = "身体愁訴",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 11, Max = 11, Value = 5 },
                        new Rate { Min = 12, Max = 15, Value = 4 },
                        new Rate { Min = 16, Max = 21, Value = 3 },
                        new Rate { Min = 22, Max = 26, Value = 2 },
                        new Rate { Min = 27, Max = 44, Value = 1 },
                    },
                    Items = new List<int> { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 },
                },
            },
        },
        new Section
        {
            Step = 3,
            Name = "周りの方々について",
            Description = "あなたの周りの方々についてうかがいます。4つの中から最もあてはまるものを選んでください。（全9問）",
            Questions = new List<Question>
            {
                new Question { Id = 1, Text = "次の人たちとはどのくらい気軽に話ができますか？上司", Score = 0, Reverse = false },
                new Question { Id = 2, Text = "次の人たちとはどのくらい気軽に話ができますか？職場の同僚", Score = 0, Reverse = false },
                new Question { Id = 3, Text = "次の人たちとはどのくらい気軽に話ができますか？配偶者、家族、友人等", Score = 0, Reverse = false },
                new Question { Id = 4, Text = "あなたが困った時、次の人たちはどのくらい頼りになりますか?上司", Score = 0, Reverse = false },
                new Question { Id = 5, Text = "あなたが困った時、次の人たちはどのくらい頼りになりますか?職場の同僚", Score = 0, Reverse = false },
                new Question { Id = 6, Text = "あなたが困った時、次の人たちはどのくらい頼りになりますか?配偶者、家族、友人等", Score = 0, Reverse = false },
                new Question { Id = 7, Text = "あなたの個人的な問題を相談したら、次の人たちはどのくらい聞いてくれますか?上司", Score = 0, Reverse = false },
                new Question { Id = 8, Text = "あなたの個人的な問題を相談したら、次の人たちはどのくらい聞いてくれますか?職場の同僚", Score = 0, Reverse = false },
                new Question { Id = 9, Text = "あなたの個人的な問題を相談したら、次の人たちはどのくらい聞いてくれますか?配偶者、家族、友人等", Score = 0, Reverse = false },
            },
            Choices = new List<string> { "非常に", "かなり", "多少", "全くない" },
            Group = "ストレス反応への影響因子",
            Factors = new List<Factor>
            {
                new Factor
                {
                    Point = 1,
                    Scale = "上司からの\nサポート",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 4, Value = 1 },
                        new Rate { Min = 5, Max = 6, Value = 2 },
                        new Rate { Min = 7, Max = 8, Value = 3 },
                        new Rate { Min = 9, Max = 10, Value = 4 },
                        new Rate { Min = 11, Max = 12, Value = 5 },
                    },
                    Items = new List<int> { 1, 4, 7 },
                },
                new Factor
                {
                    Point = 2,
                    Scale = "同僚からの\nサポート",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 5, Value = 1 },
                        new Rate { Min = 6, Max = 7, Value = 2 },
                        new Rate { Min = 8, Max = 9, Value = 3 },
                        new Rate { Min = 10, Max = 11, Value = 4 },
                        new Rate { Min = 12, Max = 12, Value = 5 },
                    },
                    Items = new List<int> { 2, 5, 8 },
                },
                new Factor
                {
                    Point = 3,
                    Scale = "家族・友人からの\nサポート",
                    Value = 0,
                    Type = "subtraction",
                    Rates = new List<Rate>
                    {
                        new Rate { Min = 3, Max = 6, Value = 1 },
                        new Rate { Min = 7, Max = 8, Value = 2 },
                        new Rate { Min = 9, Max = 9, Value = 3 },
                        new Rate { Min = 10, Max = 11, Value = 4 },
                        new Rate { Min = 12, Max = 12, Value = 5 },
                    },
                    Items = new List<int> { 3, 6, 9 },
                },
            },
        },
        new Section
        {
            Step = 4,
            Name = "満足度について",
            Description = "あなたの満足度についてうかがいます。4つの中から最もあてはまるものを選んでください。（全2問）",
            Questions = new List<Question>
            {
                new Question { Id = 1, Text = "仕事に満足だ", Score = 0, Reverse = false },
                new Question { Id = 2, Text = "家庭環境に満足だ", Score = 0, Reverse = false },
            },
            Choices = new List<string> { "満足", "まあ満足", "やや不満足", "不満足" },
            Group = "ストレス反応への影響因子",
            Factors = new List<Factor>
            {
                new Factor
                {
                    Point = 1,
                    Scale = "仕事や生活の\n満足度",
                    Value = 0,
                    Type = "addition",
                    Rates = new List<Rate>
                    {
                    new Rate { Min = 2, Max = 2, Value = 5 },
                    new Rate { Min = 3, Max = 3, Value = 4 },
                    new Rate { Min = 5, Max = 4, Value = 3 },
                    new Rate { Min = 6, Max = 6, Value = 2 },
                    new Rate { Min = 7, Max = 8, Value = 1 },
                    },
                    Items = new List<int> { 1, 2 },
                },
            }
        },
    };
    }
}