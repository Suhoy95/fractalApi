'use strict';

var FractalItemFactory = angular.module("FractalItemFactory", []);

FractalItemFactory.factory('itemFactory', function() {

    var idCounter = 1;

    return {
        emptyItem: emptyItem,
        baseItem: baseItem,
        noteItem: noteItem,
        recovery: recovery
    };

function recovery(item){
    if(item.type == "empty")
        return emptyItem(item);
    if(item.type == "note")
        return noteItem(item);
    if(item.type == "article")
        return articleItem(item);
    if(item.type == "gridItem")
        return gridItem(item);
    throw new Error("Bad type of items");
}

function emptyItem(data){
    data = data || {};

    data.type = "empty";
    data.isEmpty = function() {return this.type === "empty"; };
    data.create = function() { addItem(this); return this; };

    return data;
}

function addItem(data)
{
    data = data || {};
    data.type = "add";
    data.delete = deleteItem;
    data.createNote = createNote;
    data.createArticle = createArticle;
    data.createGrid = createGrid;

    return data;

    function deleteItem()
    {
        emptyItem(this);
        return this;
    }

    function createNote()
    {
        noteItem(this);
        return this;
    }

    function createArticle()
    {
        articleItem(this);
        return this;
    }

    function createGrid()
    {
        gridItem(this);
        return this;
    }
}

function baseItem(data){
    data = data || {};
    emptyItem(data);
    data.id = data.id || idCounter++;
    data.type = "base";
    data.analogy = data.analogy || [];
    data.sup = data.sup || [];
    data.sub = data.sub || [];
    data.bind = createRel;
    data.unbind = deleteRel;
    data.isAnalogy = isAnalogy;
    data.isSup = isSup;
    data.isSub = isSub;
    data.whatBinding = whatBinding;
    data.delete = deleteItem;
    data.deleteItem = deleteItem;

    return data;

    function createRel(item, rel) {
        var reflectionRel = getReflectionRel(rel);

        var this_index = this[rel].indexOf(item.id);
        var those_index = item[reflectionRel].indexOf(this.id)

        if(this_index < 0)
            this[rel].push(item.id);

        if(those_index < 0)
            item[reflectionRel].push(this.id);
    };

    function deleteRel(item, rel) {
        var reflectionRel = getReflectionRel(rel);

        var this_index = this[rel].indexOf(item.id);
        var those_index = item[reflectionRel].indexOf(this.id)

        if(this_index >= 0)
            this[rel].splice(this_index, 1);

        if(those_index >= 0)
            item[reflectionRel].splice(those_index, 1);  
    };

    function getReflectionRel(rel)
    {
        var  relation ={ analogy: "analogy", sup: "sub", sub: "sup" };
        if(relation[rel])
            return relation[rel];

        throw new Error("incorrect relation");
    }

    function isAnalogy(item)
    {
        return this.analogy.indexOf(item.id) >= 0 &&
               item.analogy.indexOf(this.id) >= 0;
    }

    function isSup(item)
    {
        return this.sup.indexOf(item.id) >= 0 &&
               item.sub.indexOf(this.id) >= 0;
    }

    function isSub(item)
    {
        return this.sub.indexOf(item.id) >= 0 &&
               item.sup.indexOf(this.id) >= 0;
    }

    function whatBinding(item)
    {
        if(this.isAnalogy(item))
            return "analogy";
        if(this.isSup(item))
            return "sup";
        if(this.isSub(item))
            return "sub";
        return "";
    }

    function deleteItem()
    {
        this.id = undefined;
        this.analogy = undefined;
        this.sup = undefined;
        this.sub = undefined;
        emptyItem(this);
    }
}

function noteItem(data)
{
    data = data || {};
    baseItem(data);

    data.type = "note";
    data.state = "save";
    data.title = data.title || "";
    data.text = data.text || "";
    data.edit = editNote;
    data.save = saveNote;
    data.delete = deleteNote;

    return data;

    function editNote()
    {
        this.state = "edit";
        return this;
    }

    function saveNote()
    {
        this.state = "save";
        return this
    }

    function deleteNote()
    {
        this.state = undefined;
        this.title = undefined;
        this.text = undefined;
        this.deleteItem();
        emptyItem(this);
        return this;
    }
} 

function articleItem(data)
{
    data = data || {};
    baseItem(data);

    data.type = "article";
    data.state = "save";
    data.slug = data.slug || "";
    data.title = data.title || "";
    data.text = data.text || "";
    data.edit = editArticle;
    data.save = saveArticle;
    data.delete = deleteArticle;

    return data;

    function editArticle()
    {
        this.state = "edit";
        return this;
    }

    function saveArticle()
    {
        this.state = "save";
        return this
    }

    function deleteArticle()
    {
        this.state = undefined;
        this.slug = undefined;
        this.title = undefined;
        this.text = undefined;
        this.deleteItem();
        emptyItem(this);
        return this;
    }
}

function gridItem(data)
{
    data = data || {};
    baseItem(data);

    data.type = "gridItem";
    data.state = "save";
    data.slug = data.slug || "";
    data.title = data.title || "";
    data.text = data.text || "";
    data.edit = editGrid;
    data.save = saveGrid;
    data.delete = deleteGrid;

    return data;

    function editGrid()
    {
        this.state = "edit";
        return this;
    }

    function saveGrid()
    {
        this.state = "save";
        return this
    }

    function deleteGrid()
    {
        this.state = undefined;
        this.slug = undefined;
        this.title = undefined;
        this.text = undefined;
        this.deleteItem();
        emptyItem(this);
        return this;
    }
}

});