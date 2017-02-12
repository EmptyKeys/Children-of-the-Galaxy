# EmptyKeys.Strategy.AI.Components.Considerations Namespace
 

\[Missing <summary> documentation for "N:EmptyKeys.Strategy.AI.Components.Considerations"\]


## Classes
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_BoolConsideration">BoolConsideration</a></td><td>
Implements Boolean consideration for utility based AI. If Value is higher than TrueBoundary, it sets Multiplier to 1, else 0. Rank is always 0.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_ColonizationRatioValue">ColonizationRatioValue</a></td><td>
Implements Consideration value for Utility based AI. This consideration value represents colonization ratio of context Player.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_Constant">Constant</a></td><td>
Implements Consideration for Utility based AI. This consideration is constant value.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_DecayCurve">DecayCurve</a></td><td>
Implements Consideration for Utility based AI. This consideration is Decay curve. Rank = (Pow(Max, K) - Pow(Value, K)) / Pow(Max, K)</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_ExplorationRatioValue">ExplorationRatioValue</a></td><td>
Implements Consideration Value for Utility based AI. This value represents Exploration ratio.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_LogCurve">LogCurve</a></td><td>
Implements Consideration for Utility based AI. This consideration is Log curve. Rank = (Log(Value / (1 - Value)) + Middle) / Denominator</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_PlanetUpgradesValue">PlanetUpgradesValue</a></td><td>
Implements Consideration value for Utility based AI. This value represents number of possible planet upgrades of specified type (UpgradeModifier).</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_PlanetValue">PlanetValue</a></td><td>
Implements Consideration value for Utility based AI. This value represents Planet value of specified Property. Implemented with reflection.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_PlayerValue">PlayerValue</a></td><td>
Implements Consideration value for Utility based AI. This value represents Player value of specified Property. Implemented with reflection.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_ProductionAverageValue">ProductionAverageValue</a></td><td>
Implements Consideration value for Utility based AI. This value represents production average of the context Player.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="T_EmptyKeys_Strategy_AI_Components_Considerations_UnitValue">UnitValue</a></td><td>
Implements Consideration value for Utility based AI. This value represents Unit value of specified Property. Implemented with reflection.</td></tr></table>&nbsp;
