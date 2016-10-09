
function search() {
   
    $.ajax({
        
        url: "/Books/Index?searchString=" + $('#SearchString').val() + "&pageType=" + pageType,
        success: function (result) {
      
            ChangeUrl("index", "/Books/Index?searchString=" + $('#SearchString').val().trim());
          
            if ($(result).find(".pagination-container .pagination ").children().length == 0) {
               
                $('#bookList').html(result + '<div style="color:red;font-size:18pt; text-align:center;">There is no resualt</div>');
               ;
            } else {
                $('#bookList').html(result);
            }
        }
    });
}
$("#serchBtn").on("click", function () {

    search();
});
$("#SearchString").keypress(function (e) {
    if (e.keyCode == 13) {
        search();
    }
});
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
$('body').on('click', '#ProductList .table th', function (event) {

    var tdIndex = $(this).index() + 1;
 
    if (tdIndex != 1) {
        var searchString = $('#SearchString').val();
        if (searchString == undefined || searchString == '') {
            searchString = '';
        } else {
            searchString = '&searchString=' + searchString;
        }

        var columnToSort = $(this).text().trim();
        var currentSortOption = getUrlVars()['sortOption'];
        console.log(currentSortOption);
        var sort;

        switch (currentSortOption) {
            case "Title_asc":
                sort = 'sortOption=Title_desc';

                break;
            case "Title_desc":
                sort = 'sortOption=Title_asc';
                break;
            case "Price_asc":
                sort = 'sortOption=Price_desc';
                break;
            case "Price_desc":
                sort = 'sortOption=Price_asc';
                break;
            case "PageCount_asc":
                sort = 'sortOption=PageCount_desc';
                break;
            case "PageCount_desc":
                sort = 'sortOption=PageCount_asc';
                break;
            case "FirstName_asc":
                sort = 'sortOption=FirstName_desc';
                break;
            case "FirstName_desc":
                sort = 'sortOption=FirstName_asc';
                break;
            case "LastName_asc":
                sort = 'sortOption=LastName_desc';
                break;
            case "LastName_desc":
                sort = 'sortOption=LastName_asc';
                break;
            case "Name_asc":
                sort = 'sortOption=Name_desc';
                break;
            case "Name_desc":
                sort = 'sortOption=Name_asc';
                break;
            case "Gener_asc":
                sort = 'sortOption=Gener_desc';
                break;
            case "Gener_desc":
                sort = 'sortOption=Gener_asc';
                break;
            default:
                sort = '';
                break;
        }


        switch (columnToSort) {
            case 'Title':

                if (currentSortOption != 'Title_asc' && currentSortOption != 'Title_desc') {

                    sort = 'sortOption=Title_asc';
                }
                break;
            case 'Price':
                if (currentSortOption != 'Price_asc' && currentSortOption != 'Price_desc') {
                    sort = 'sortOption=Price_asc';
                }
                break;
            case 'PageCount':
                if (currentSortOption != 'PageCount_asc' && currentSortOption != 'PageCount_decs') {
                    sort = 'sortOption=PageCount_asc';
                }
                break;
            case 'FirstName':
                if (currentSortOption != 'FirstName_asc' && currentSortOption != 'FirstName_decs') {
                    sort = 'sortOption=FirstName_asc';
                }
                break;
            case 'LastName':
                if (currentSortOption != 'LastName_asc' && currentSortOption != 'LastName_decs') {
                    sort = 'sortOption=LastName_asc';
                }
                break;
            case 'Name':
                if (currentSortOption != 'Name_asc' && currentSortOption != 'Name_decs') {
                    sort = 'sortOption=Name_asc';
                }
                break;
            case 'Gener':
                if (currentSortOption != 'Gener_asc' && currentSortOption != 'Gener_decs') {
                    sort = 'sortOption=Gener_asc';
                }
                break;
            default:
                sort = '';
                break;

        }
        if (sort != '') {
            sort = '&' + sort;
        }

        var url = '/Books/Index?pageType=table' + searchString + sort;
        $.ajax({
            url: url,
            success: function (result) {

                ChangeUrl('index', url);
                $('#ProductList').html(result);
                console.log(":::::::", tdIndex);

                if (currentSortOption == null) {

                    $('.table th:nth-child(' + tdIndex + ') .fa-sort-asc').css('color', '#777777');
                } else {

                    var sortOrder = currentSortOption.split("_")[1] == 'asc' ? 'desc' : 'asc';
                    $('.table th:nth-child(' + tdIndex + ') .fa-sort-' + sortOrder).css('color', '#777777');
                }

            }
        });
    }
});
function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState(null, obj.Page, obj.Url);
    } else {
        alert("Browser does not support HTML5.");
    }
}
