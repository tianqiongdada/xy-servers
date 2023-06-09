using System;
using Ddxy.Protocol;

namespace Ddxy.GameServer.Logic.Battle.Skill
{
    public class HighPanShanSkill : BaseSkill
    {
        public HighPanShanSkill() : base(SkillId.HighPanShan, "蹒跚")
        {
            Kind = SkillId.PanShan;
            ActionType = SkillActionType.Passive;
            EffectTypes.Add(AttrType.Spd);
            Quality = SkillQuality.High;
        }

        public override SkillEffectData GetEffectData(GetEffectDataRequest request)
        {
            var relive = request.Relive.GetValueOrDefault();
            var level = request.Level.GetValueOrDefault();
            var intimacy = request.Intimacy.GetValueOrDefault();

            var effectData = new SkillEffectData
            {
                Del = MathF.Floor(50 * (relive * 0.3f + 1) * (MathF.Pow(level, 0.5f) / 10 +
                                                                     MathF.Pow(intimacy, 0.166666f) * 10 /
                                                                     (100 + relive * 20)))
            };
            return effectData;
        }
    }
}