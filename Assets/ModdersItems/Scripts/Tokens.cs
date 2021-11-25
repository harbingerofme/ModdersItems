using RoR2.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Moonstorm;

namespace ModdersItems.Tokens
{
    public class TokensModule : ModuleBase
    {
        public static TokensModule Instance { get; set; }
        public override AssetBundle AssetBundle { get; set; } = Modules.Assets.mainAssetBundle;
        public override SerializableContentPack ContentPack { get; set; } = Modules.Assets.serialContentPack;

        public override void Init()
        {
            Instance = this;
            base.Init();
            InitTokens();
        }

        public IEnumerable<TokenBase> InitTokens()
        {
            Debug.Log("ModdersItems: Initializing Tokens...");
            GetContentClasses<TokenBase>().ToList().ForEach(token => AddToken(token));
            return null;
        }

        public void AddToken(TokenBase token)
        {
            token.Initialize();
        }
    }

    public class TokenBase : ContentBase
    {
        public const string prefix = ModdersItemsPlugin.developerPrefix + "_";
        public virtual string name { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            Debug.Log("ModdersItems: Added Tokens for: " + GetType().Name);
            AddTokens();
        }

        public virtual void AddTokens()
        {
            
        }
    }
}
