using System;
using Ddxy.Protocol;

namespace Ddxy.GameServer.Logic.Battle.Skill
{
    /// <summary>
    /// 用自己的发力 换 对方的气血
    /// </summary>
    public class QingMianLiaoYaSkill : BaseSkill
    {
        public QingMianLiaoYaSkill() : base(SkillId.QingMianLiaoYa, "青面獠牙")
        {
            Kind = SkillId.QingMianLiaoYa;
            ActionType = SkillActionType.Initiative;
            TargetType = SkillTargetType.Enemy;
            Quality = SkillQuality.Low;
        }

        public override bool UseSkill(BattleMember member, out string error)
        {
            if (member.Mp < 10)
            {
                error = "法力不足，无法释放";
                return false;
            }

            var sub = MathF.Ceiling(member.Mp * 0.95f);
            // 天策符 千钧符  冥想符   减少耗蓝
            sub = GetNeedMpPost(member, sub);
            member.AddMp(-sub);
            error = null;
            return true;
        }

        public override SkillEffectData GetEffectData(GetEffectDataRequest request)
        {
            var relive = request.Relive.GetValueOrDefault();
            var level = request.Level.GetValueOrDefault();
            var intimacy = request.Intimacy.GetValueOrDefault();

            var percent = MathF.Floor(10.1f + 2 * (relive * 0.4f + 1) * (MathF.Pow(level, 0.5f) / 10 +
                                                                         MathF.Pow(intimacy, 0.166666f) * 10 /
                                                                         (100 + relive * 20)));
            var preMp = MathF.Ceiling(request.Attrs.Get(AttrType.Mp) / 0.05f);
            // 算出牺牲值
            var delta = preMp - request.Attrs.Get(AttrType.Mp);

            var effectData = new SkillEffectData
            {
                Hurt = MathF.Round(delta * percent / 100)
            };
            return effectData;
        }
    }
}