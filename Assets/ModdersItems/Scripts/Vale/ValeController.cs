using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moonstorm;
using RoR2;
using RoR2.Skills;

namespace ModdersItems.Items
{
    public class ValeController : ItemBase
    {
        public override ItemDef ItemDef { get; set; } = Modules.Assets.mainAssetBundle.LoadAsset<ItemDef>("ValeController");

        public static float speedMulti = 0.4f;
        public static float speedStackMulti = 0.2f;
        public static float buffDuration = 1.5f;
        public static float buffDurationStack = 0.25f;
        public static float buffCooldown = 5f;

        public override void AddBehavior(ref CharacterBody body, int stack)
        {
            body.AddItemBehavior<Behavior>(stack);
        }

        public class Behavior : CharacterBody.ItemBehavior, IStatItemBehavior
        {
            public bool canSprintAny;
            public bool originalSprint;
            public float cooldown;

            public void Start()
            {
                originalSprint = ((body.bodyFlags & CharacterBody.BodyFlags.SprintAnyDirection) == CharacterBody.BodyFlags.SprintAnyDirection);
            }

            public void RecalculateStatsStart()
            {
            }

            public void RecalculateStatsEnd()
            {
                if (body.isSprinting && cooldown <= 0f)
                {
                    if (!body.HasBuff(Buffs.BuffValeSprint.buff))
                    {
                        cooldown = ValeController.buffCooldown;
                        for (int i = 0; i < stack; i++)
                        {
                            body.AddTimedBuff(Buffs.BuffValeSprint.buff, ValeController.buffDuration + (ValeController.buffDurationStack * (stack - 1)), stack);
                        }
                    }
                }
                canSprintAny = ((body.bodyFlags & CharacterBody.BodyFlags.SprintAnyDirection) == CharacterBody.BodyFlags.SprintAnyDirection);
                if (!canSprintAny) body.bodyFlags |= CharacterBody.BodyFlags.SprintAnyDirection;
            }

            public void FixedUpdate()
            {
                if (cooldown > 0f)
                {
                    cooldown -= Time.fixedDeltaTime;
                }
            }

            public void OnDestroy()
            {
                if (canSprintAny && (originalSprint != canSprintAny)) body.bodyFlags &= CharacterBody.BodyFlags.SprintAnyDirection;
            }
        }
    }
}
