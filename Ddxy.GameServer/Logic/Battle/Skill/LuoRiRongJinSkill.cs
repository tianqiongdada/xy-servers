using System;
using Ddxy.Protocol;

namespace Ddxy.GameServer.Logic.Battle.Skill
{
    public class LuoRiRongJinSkill : BaseSkill
    {
        public LuoRiRongJinSkill() : base(SkillId.LuoRiRongJin, "落日熔金")
        {
            Type = SkillType.GhostFire;
        }

        public override SkillEffectData GetEffectData(GetEffectDataRequest request)
        {
            var level = request.Level.GetValueOrDefault();
            var profic = request.Profic.GetValueOrDefault();
            var deadNum = request.Deadnum.GetValueOrDefault();

            var hurt = MathF.Floor(80 * level * (MathF.Pow(profic, 0.4f) * 2.8853998118144273f / 100 + 1));
            hurt *= 1.25f;
            if (deadNum > 0) hurt *= (1 + deadNum * 0.2f);

            var effectData = new SkillEffectData
            {
                Hurt = hurt
            };
            // 伤害衰减1/3
            effectData.Hurt *= 0.667f;
            
            // 加强鬼火
            var attrs = request.Attrs;
            if (attrs != null)
            {
                var value = attrs.Get(AttrType.JqGuiHuo);
                if (value > 0)
                {
                    var percent = value / 100.0f;
                    effectData.Hurt = (int) MathF.Round(effectData.Hurt * (1 + percent));
                }
            }
            
            if (request.Attrs != null && request.OrnamentSkills != null)
            {
                var lingXing = (int) request.Attrs.Get(AttrType.LingXing);
                if (lingXing >= 550)
                {
                    var delta = 0;
                    if (request.OrnamentSkills.ContainsKey(2021))
                    {
                        delta += lingXing * 20;
                    }

                    if (request.OrnamentSkills.ContainsKey(2022))
                    {
                        delta += lingXing * 40;
                    }

                    if (delta > 0) effectData.Hurt += delta;
                }
            }
            
            return effectData;
        }
    }
}