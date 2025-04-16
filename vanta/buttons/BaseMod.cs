using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static vanta.buttons.ModManager;

namespace vanta.buttons
{
    public abstract class BaseMod : MonoBehaviour
    {
        public virtual string Name { get; set; } = "Button";
        public virtual Categories Category { get; set; } = Categories.Main;
        public virtual string Description { get; set; } = "Button";

        public virtual bool ModuleIsEnabled { get; set; } = false;


        public virtual void OnModuleEnable() //called on enable
        {

        }

        public virtual void OnModuleUpdate() //called every frame on enable
        {

        }

        public virtual void OnModuleDisable() //called on disable
        {

        }


    }
}