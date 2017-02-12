# PlanetDecisionLeaf Class
 

Implements planet decision leaf for Decision tree behavior. This decision calculates value of possible planet upgrade (UpgradeModifier type). If this decision is selected, Factory Item is stored in ItemToBuild of PlanetBehaviorContext.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;BaseDecisionTreeNode<br />&nbsp;&nbsp;&nbsp;&nbsp;EmptyKeys.Strategy.AI.Components.Decisions.PlanetDecisionLeaf<br />
**Namespace:**&nbsp;<a href="N_EmptyKeys_Strategy_AI_Components_Decisions">EmptyKeys.Strategy.AI.Components.Decisions</a><br />**Assembly:**&nbsp;EmptyKeys.Strategy.AI (in EmptyKeys.Strategy.AI.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public class PlanetDecisionLeaf : BaseDecisionTreeNode
```

The PlanetDecisionLeaf type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_EmptyKeys_Strategy_AI_Components_Decisions_PlanetDecisionLeaf__ctor">PlanetDecisionLeaf</a></td><td>
Initializes a new instance of the PlanetDecisionLeaf class.</td></tr></table>&nbsp;
<a href="#planetdecisionleaf-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td>GoalPercentage</td><td> (Inherited from BaseDecisionTreeNode.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td>IsNodeValid</td><td> (Inherited from BaseDecisionTreeNode.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_EmptyKeys_Strategy_AI_Components_Decisions_PlanetDecisionLeaf_UpgradeModifier">UpgradeModifier</a></td><td>
Gets or sets the upgrade modifier.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td>Value</td><td> (Inherited from BaseDecisionTreeNode.)</td></tr></table>&nbsp;
<a href="#planetdecisionleaf-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_EmptyKeys_Strategy_AI_Components_Decisions_PlanetDecisionLeaf_CalculateValue">CalculateValue</a></td><td>
Calculates the value.
 (Overrides BaseDecisionTreeNode.CalculateValue(IBehaviorContext).)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as the default hash function.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_EmptyKeys_Strategy_AI_Components_Decisions_PlanetDecisionLeaf_MakeDecision">MakeDecision</a></td><td>
Makes the decision.
 (Overrides BaseDecisionTreeNode.MakeDecision(IBehaviorContext).)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr></table>&nbsp;
<a href="#planetdecisionleaf-class">Back to Top</a>

## See Also


#### Reference
<a href="N_EmptyKeys_Strategy_AI_Components_Decisions">EmptyKeys.Strategy.AI.Components.Decisions Namespace</a><br />BaseDecisionTreeNode<br />