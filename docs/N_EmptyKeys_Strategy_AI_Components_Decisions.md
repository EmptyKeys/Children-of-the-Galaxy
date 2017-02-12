# EmptyKeys.Strategy.AI.Components.Decisions Namespace
 

\[Missing <summary> documentation for "N:EmptyKeys.Strategy.AI.Components.Decisions"\]


## Classes
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Decisions_PlanetDecisionLeaf">PlanetDecisionLeaf</a></td><td>
Implements planet decision leaf for Decision tree behavior. This decision calculates value of possible planet upgrade (UpgradeModifier type). If this decision is selected, Factory Item is stored in ItemToBuild of PlanetBehaviorContext.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Decisions_ResearchGoalLeaf">ResearchGoalLeaf</a></td><td>
Implements player decision leaf for Decision tree behavior. This decision calculates research technology goal value of specified technology (GoalTechnologyId). If this goal is selected, the result is stored in GoalTechnologyId of PlayerBehaviorContext.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Decisions_UnitBehaviorLeaf">UnitBehaviorLeaf</a></td><td>
Implements unit decision leaf for Decision tree behavior. This decision calculates value of specified behavior (BehaviorName) for all player's units. If this decision is selected, it sets Unit behavior.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Decisions_UnitDecisionLeaf">UnitDecisionLeaf</a></td><td>
Implements planet decision leaf for Decision tree behavior. This decision calculates value of Unit type (UnitTypeName) with specific Behavior (BehaviorName). If this decision is selected, planet is set to build it (ItemToBuild).</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Decisions_UnitUpgradeLeaf">UnitUpgradeLeaf</a></td><td>
Implements player decision leaf for Decision tree behavior. This decision calculates value for specified upgrade (UpgradeId). If this decision is selected, upgrade is stored in UnitUpgrade of PlayerBehaviorContext.</td></tr></table>&nbsp;
