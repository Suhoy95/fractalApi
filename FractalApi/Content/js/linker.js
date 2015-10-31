'use strict';

var FractalLinker = angular.module("FractalLinker", []);

FractalLinker.factory('linker', function() {
var relations = ["analogy", "sup", "sub"];
    return {
        binding: "",
        currentItem: {},
        isSelecting: isSelecting,
        select: select,
        disable: disable,
        bind: bind, 
        unbind: unbind
    };

function isSelecting()
{
    return relations.indexOf(this.binding) >= 0;
}

function select(item, relation)
{
    if(relations.indexOf(relation) < 0) 
        throw new Error("bad relation: " + relation);

    this.binding = relation;
    this.currentItem = item;
}

function disable()
{
    this.binding = "";
    this.currentItem = {};
}

function bind(item)
{
    this.currentItem.bind(item, this.binding);
}

function unbind(item)
{
    var curItem = this.currentItem;
    var binding = curItem.whatBinding(item);

    if(binding !== "")
        curItem.unbind(item, binding);
}

});