'use strict';

var FractalShower = angular.module("FractalShower", []);

FractalShower.factory('shower', function() {
var relations = ["analogy", "sup", "sub"];
    return {
        bindings: { analogy: false, sub: false, sup: false},
        currentItem: {},
        state: getState,
        setBinding: setBinding,
        unsetBinding: unsetBinding,
        clearBinding:clearBinding,
        filterItems: filterItems

    };

function getState()
{
    var bindings = this.bindings;
    for(var relation in bindings)
        if(bindings[relation])
            return "relations";
    return "grid";
}

function setBinding(item, relation)
{
    if(relations.indexOf(relation) < 0)
        throw new Error("Bad relation");

    if(item !== this.currentItem)
        this.clearBinding();

    this.bindings[relation] = true;
    this.currentItem = item;
}

function unsetBinding(relation)
{
    if(relations.indexOf(relation) < 0)
        throw new Error("Bad relation");

    this.bindings[relation] = false;    
}

function clearBinding()
{
    for(var key in this.bindings)
        this.bindings[key] = false;
}

function filterItems(items)
{
    var result = [];
    var item = this.currentItem;
    for(var x = 0; x < items.length; x++)
        for(var y = 0; y < items[x].length; y++)
            if(!items[x][y].isEmpty())
            {
                var binding = item.whatBinding(items[x][y]);
                if( binding !== "" && this.bindings[binding])
                    result.push(items[x][y]);
            }
    return result;
}

});
