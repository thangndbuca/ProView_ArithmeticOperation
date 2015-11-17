//(function (window, ko) {
//    window.Utils = window.Utils || {};
//    window.Utils.pagedObservable = function (options) {
//        options = options || {};
//        var _allData = ko.observableArray(), //the data collection to dispaly in grid

//            _columns = ko.observableArray(options.columns || []), //the columns of grid

//          _pageSize = ko.observable(options.pageSize || 10), //the size of the pages to display
//          _pageSizes = ko.observable(options.pageSizes || []), //the size of the pages to display
//          _pageIndex = ko.observable(1), //the index of the current page
//          _pageCount = ko.observable(0), //the number of pages

//           _totalRecords = ko.observable(0), //the number of records

//           _sortName = ko.observable(options.sortName || ''), //the sort column name

//           _sortOrder = ko.observable(options.sortOrder || 'asc'), //the sort order
//         //move to the next page
//       _nextPage = function () {
//           if (_pageIndex() < _pageCount()) {
//               _pageIndex(parseInt(_pageIndex()) + 1);
//           }
//       },
//   //move to the previous page
//       _previousPage = function () {
//           if (_pageIndex() > 1) {
//               _pageIndex(_pageIndex() - 1);
//           }
//       },
//            //move to first page
//            _firstPage = function () {
//                if (_pageIndex() > 1) {
//                    _pageIndex(1);
//                }
//            },
//            //move to last page
//            _lastPage = function () {
//                if (_pageIndex() < _pageCount()) {
//                    _pageIndex(_pageCount());
//                }
//            },
//            //sort a column
//            _sort = function (column) {
//                _sortName(column.index);
//                _sortOrder(_sortOrder() === 'asc' ? 'desc' : 'asc');
//                _pageIndex(1);
//                _loadFromServer();
//            },
//            //the message for record info
//            _recordMessage = ko.computed(function () {
//                if (_allData().length > 0) {
//                    return 'Records ' + ((_pageIndex() - 1) * _pageSize() + 1) + ' - ' + (_pageIndex() < _pageCount() ? _pageIndex() * _pageSize() : _totalRecords()) + ' of ' + _totalRecords();
//                }
//                else {
//                    return 'No records';
//                }
//            }),
//            //the message for page info
//            _pageMessage = ko.computed(function () {
//                if (_allData().length > 0) {
//                    return 'Page ' + _pageIndex() + ' of ' + _pageCount();
//                }
//                else {
//                    return 'No pages';
//                }
//            }),
//            //service url
//            _serviceURL = ko.computed(function () {
//                return options.serviceURL + (options.serviceURL.indexOf('?') != -1 ? "&" : "?") + "sidx=" + _sortName() + "&sord=" + _sortOrder() + "&page=" + _pageIndex() + "&rows=" + _pageSize();
//            }, this),
//           //load data from server
//            _loadFromServer = function () {
//                $.getJSON(_serviceURL(), function (data) {
//                    if (data != null) {
//                        _totalRecords(data.records);
//                        _pageCount(data.total);
//                        _allData(data.rows || []);
//                    }
//                    else {
//                        _totalRecords(0);
//                        _pageCount(0);
//                        _allData([]);
//                    }
//                });
//            };
//        _pageIndex.subscribe(function () {
//            _pageIndex() < 1 ? _pageIndex(1) : _loadFromServer();
//        });
//        _pageSize.subscribe(function () {
//            _pageIndex() != 1 ? _pageIndex(1) : _loadFromServer();
//        });
//        _loadFromServer();
//        //public members
//        this.columns = _columns;
//        this.rows = _allData;
//        this.totalRecords = _totalRecords;
//        this.pageSize = _pageSize;
//        this.pageSizes = _pageSizes;
//        this.pageIndex = _pageIndex;
//        this.pageCount = _pageCount;
//        this.nextPage = _nextPage;
//        this.previousPage = _previousPage;
//        this.firstPage = _firstPage,
//        this.lastPage = _lastPage,
//        this.sortOrder = _sortOrder;
//        this.sortName = _sortName;
//        this.sort = _sort;
//        this.recordMessage = _recordMessage;
//        this.pageMessage = _pageMessage;
//        this.load = _loadFromServer;
//    };
//}(window, ko));

Patterns.namespace("ProView").Arithmetics = (function () {
    var utils = Patterns.ProView.Utils;
    var history = Patterns.ProView.History;

    var activateHistory = function () {
        history.init(popState);

        // popState is called when browser backward of forward button is clicked
        var popState = function (state) {

            $('#content').load(state.url + "&layout=false", function () {
                activateControls();
                activateSorter();
            });
        };

        // replace state when first page is rendered.
        // note: we remove antiforgery token 
        var data = $($("form")[0].elements).not("input[name*='RequestVerificationToken']").serialize()
        var state = "?" + data;
        history.replace(state);
    };

    var activateControls = function () {
        $("select[name='sort']").change(function () {
            $("#page").val(1);
            $("form").submit();
        });

        $("form").submit(ajaxSubmit);
    };

    var activatePager = function () {

        // pagination

        $("a[name^='page']").on("click", function (event) {
            var parent = $(this).parent();
            if (parent.hasClass('disabled')) return utils.stopEvent(event);
            if (parent.hasClass('active')) return utils.stopEvent(event);

            var page = $(this).attr('href').substr(1);
            $("#page").val(page);
            $("form").submit();

            return utils.stopEvent(event);
        });

        // activate delete buttons also

        $("button[name='delete']").bind("click", function (event) {
            // animate row away
            $(this).closest("tr").fadeOut(1000, function () {
                $(this).remove();
            });

            // also post antiforgery token to server

            var token = $(':input:hidden[name*="RequestVerificationToken"]');
            var data = {};
            data[token.attr("name")] = token.val();

            var id = $(this).closest("tr").data("id");
            data["id"] = id;

            var options = {
                url: "/arithmetic",
                type: "DELETE",
                data: data
            };

            // CQRS: fire and forget...

            $.ajax(options);

            return utils.stopEvent(event);

        });

    };

    var ajaxSubmit = function (event) {

        var $form = $(this);
        // push current state to history
        // note: we remove antiforgery token 
        var data = $($("form")[0].elements).not("input[name*='RequestVerificationToken']").serialize()
        var state = "?" + data;
        history.push(state);

        var options = {
            url: "/arithmetics",
            type: "GET",
            data: data
        };

        $.ajax(options).done(function (data) {
            $("#arithmetic").replaceWith(data);
            activatePager();
        });

        return utils.stopEvent(event);
    };

    var start = function () {
        activateHistory();
        activatePager();
        activateControls();
    };

    // the revealing part of the revealing module pattern   
    return { start: start };
})();