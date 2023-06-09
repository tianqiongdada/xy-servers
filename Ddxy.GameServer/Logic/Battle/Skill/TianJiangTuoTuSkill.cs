using System;
using Ddxy.Protocol;

namespace Ddxy.GameServer.Logic.Battle.Skill
{
    public class TianJiangTuoTuSkill : BaseSkill
    {
        public TianJiangTuoTuSkill() : base(SkillId.TianJiangTuoTu, "天降脱兔")
        {
            Kind = SkillId.TianJiangTuoTu;
            Type = SkillType.Physics;
            ActionType = SkillActionType.Passive;
        }

        public override SkillEffectData GetEffectData(GetEffectDataRequest request)
        {
            var relive = request.Relive.GetValueOrDefault();
            var level = request.Level.GetValueOrDefault();
            var intimacy = request.Intimacy.GetValueOrDefault();
            var atk = request.Atk.GetValueOrDefault();

            var percent = 30 + intimacy / 10000000.0f;
            if (percent > 45.0f) percent = 45.0f;
            
            return new SkillEffectData
            {
                Percent = percent,
                TargetNum = 3,
                Hurt = MathF.Floor(80 * level + atk / 100.0f * 9f * (relive * 0.6f + 1) *
                    (MathF.Pow(level, 0.5f) / 10 +
                     MathF.Pow(intimacy, 0.166666f) * 10 /
                     (100 + relive * 20)))
            };
        }
    }
}