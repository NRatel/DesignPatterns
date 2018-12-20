using System.Collections.Generic;
using UnityEngine;

namespace Interpreter
{
    // 环境/上下文。存储可直接进行翻译的内容
    public class Context
    {
        private Dictionary<int, string> typeDict;
        public Context() {
            typeDict = new Dictionary<int, string>();
            typeDict.Add(0, "钻石");
            typeDict.Add(1, "金币");
            typeDict.Add(2, "月卡");
            typeDict.Add(3, "体验VIP");
            typeDict.Add(4, "优惠券");
            typeDict.Add(5, "门票");
        }

        public string GetRewardTypeName(int type)
        {
            return typeDict[type];
        }
    }

    //抽象表达式，提供通用的解释接口
    public abstract class Expression
    {
        public abstract string Interpret(Context context);
    }

    //“奖励列表字符串”表达式。（非终结表达式）
    public class RewardsStrExp : Expression
    {
        string rewardsStr;
        public RewardsStrExp(string rewardsStr)
        {
            this.rewardsStr = rewardsStr.Trim();
        }

        public override string Interpret(Context context)
        {
            string result = "奖励列表: {\n";
            string[] rewardStrs = this.rewardsStr.Split(';');
            foreach (var rewardStr in rewardStrs)
            {
                //非终结表达式聚合依赖了另外的表达式（可能是多个、可能递归依赖自身）
                result += new RewardStrExp(rewardStr).Interpret(context) + ";\n";
            }
            result += "}";
            return result;
        }
    }

    //“单个奖励字符串”表达式（非终结表达式）
    public class RewardStrExp : Expression
    {
        string rewardStr;
        public RewardStrExp(string rewardStr)
        {
            this.rewardStr = rewardStr.Trim();
        }

        public override string Interpret(Context context)
        {
            string[] typeAndNum = this.rewardStr.Split(',');
            int type = int.Parse(typeAndNum[0]);
            int num = int.Parse(typeAndNum[1]);
            //非终结表达式无法直接解释，因此聚合依赖了另外的表达式进行解释。
            //非终结表达式，也不完全依赖其他表达式进行解释，比如此处的“Num”就可直接利用自然语义给出解释。
            return "    名称: " + new RewardTypeExp(type).Interpret(context) + ", 数量: " + num;
        }
    }

    //“奖励类型”表达式（终结表达式）
    public class RewardTypeExp : Expression
    {
        int type;
        public RewardTypeExp(int type)
        {
            this.type = type;
        }

        public override string Interpret(Context context)
        {
            //重点: 终结表达式直接利用 context 或 自然语义给出直接解释。
            return context.GetRewardTypeName(this.type);
        }
    }
    
    //客户
    public class Client
    {
        static public void Main()
        {
            Context context = new Context();
            //解释“奖励字符串”表达式
            string rewardsStr = "1,123;3,1;4,999;2,1";
            string result = new RewardsStrExp(rewardsStr).Interpret(context);
            Debug.Log(result);
        }
    }
}
