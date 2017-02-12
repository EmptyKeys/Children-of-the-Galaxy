using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Environment.Factory;
using EmptyKeys.Strategy.Environment.PlanetUpgrades;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlanet
{
    /// <summary>
    /// Implements planet action for behavior. This action finds available Factory Item based on Upgrade Modifier attribute.
    /// The result is stored in ItemToBuild of PlanetBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetUpgradeToBuild : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the upgrade modifier.
        /// </summary>
        /// <value>
        /// The upgrade modifier.
        /// </value>
        [XmlAttribute]
        public UpgradeModifierType UpgradeModifier { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPlanetUpgradeToBuild"/> class.
        /// </summary>
        public FindPlanetUpgradeToBuild()
            : base()
        {
        }

        /// <summary>
        /// Executes behavior with given context
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override BehaviorReturnCode Behave(IBehaviorContext context)
        {
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (planetContext == null && playerContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player owner = null;
            Planet planet = null;
            if (planetContext != null)
            {
                owner = planetContext.Planet.Owner;
                planet = planetContext.Planet;
            }
            else if (playerContext != null)
            {
                owner = playerContext.Player;
                planet = playerContext.BehaviorTarget as Planet;
            }

            if (owner == null || planet == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            
            
            List<FactoryItem> possibleUpgrades = owner.GetPossiblePlanetUpgrades(planet, UpgradeModifier);
            if (possibleUpgrades.Count == 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            FactoryItem upgrade = possibleUpgrades.OrderBy(u => u.ProductionCost).First();
            if (upgrade != null)
            {
                context.AddLogMessage("Upgrade found - " + upgrade.Name);
                if (planetContext != null)
                {
                    planetContext.ItemToBuild = upgrade;
                }
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }        
    }
}
