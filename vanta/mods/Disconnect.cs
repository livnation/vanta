using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vanta.buttons;
using static vanta.buttons.ModManager;

namespace vanta.mods
{
    public class Disconnect : BaseMod
    {
        public override string Name { get; set; } = "Disconnect";
        public override Categories Category { get; set; } = Categories.Main;

        public override string Description { get; set; } = "Disconnect from the lobby.";

        public override bool ModuleIsEnabled { get; set; } = false;

        public override void OnModuleEnable()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnModuleUpdate()
        {

        }

        public override void OnModuleDisable()
        {

        }
    }
}
