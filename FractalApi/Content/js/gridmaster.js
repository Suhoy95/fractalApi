
var FractalGridMaster = angular.module("FractalGridMaster", ["FractalItemFactory"]);

FractalGridMaster.factory('gridMaster', ["itemFactory", function(itemFactory) {

    return { 
        completeGrid: function(items, setting)
        {
            items = items || [];

            if(!setting.fixedWidth)
                setting.width = max(setting.minWidth, findMaxWidth(items));
    
            setting.height = max(setting.minHeight, findMaxHeight(items));
  
            items = completeColumns(items, setting.width);
            items = completeRows(items, setting.height);
  
            return items;
        },
        //TODO: replace 'setting' to 'minWidth'
        createMinGrid: function(items, setting)
        {
            var x = 0, y = 0;
            var result = [];
            for(var i = 0; i < setting.minWidth; i++)
                result[i] = [];

            for(var i in items)
            {
                result[x][y] = items[i];
                x++;
                if(x === setting.minWidth)
                {
                    y++;
                    x = 0;
                }
            }
            return result;
        },

        recoveryId: function(item, items)
        {
            var aim = item.id;
            var what = item.id = item.realId;
            item.realId = undefined;
            for(var x = 0; x < items.length; x++)
                for(var y = 0; y < items[x].length; y++)
                    if(!items[x][y].isEmpty())
                    {
                        var it = items[x][y];
                        replaceId(it.analogy, aim, what);
                        replaceId(it.sup, aim, what);
                        replaceId(it.sub, aim, what);                        
                    } 
        },

        removeItem: removeItem,
        getItemsCoord: getItemsCoord
    };

    function completeColumns(items, amountOfColumn)
    {
        for(var i = 0; i < amountOfColumn; i++)
            items[i] = items[i] || [];

        var amountRmColumn = items.length - amountOfColumn;
        if(amountRmColumn > 0)
            items.splice(amountOfColumn, amountRmColumn);

        return items;   
    };

    function completeRows(items, amountOfRows)
    {
        for(var key in items)
        {
            var tmpColumn = items[key];
            items[key] = [];
            for(var i = 0; i < amountOfRows; i++)
                items[key][i] = tmpColumn[i] || itemFactory.emptyItem();
        }

        return items;
    };

    function findMaxWidth(items)
    {
        for(var x = items.length - 1; x >= 0; x--)
            for(var y = 0; y < items[x].length; y++)
                if(!items[x][y].isEmpty())
                    return x + 2;
        return 0;
    }

    function findMaxHeight(items)
    {
        var max = -1;
        for(var x = 0; x < items.length; x++)
            for(var y = 0; y < items[x].length; y++)
            {
                if(!items[x][y].isEmpty() && y > max)
                    max = y;
            }
        return max == -1 ? 0 : max + 2;
    }

    function max(a, b)
    {
        return a > b ? a : b;
    }

    function replaceId(array, aim, what)
    {
        var index = array.indexOf(aim);
        if(index != -1)
            array[index] = what;
    }

    function removeItem(items, item)
    {
        for(var x = 0; x < items.length; x++)
        {
            var column = items[x];
            var i = column.indexOf(item);
            if(i >= 0)
            {
                column.splice(i, 1);
                return;
            }
        }
    }

    function getItemsCoord(items)
    {
        var result = []
        for(var x = 0; x < items.length; x++)
            for(var y = 0; y < items[x].length; y++)
                if(!items[x][y].isEmpty() && items[x][y].id >= 0)
                {
                    result.push([items[x][y].id, x, y]);
                }
        return result;
    }
}]);