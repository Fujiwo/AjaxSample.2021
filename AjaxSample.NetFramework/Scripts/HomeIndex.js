// JavaScript + Partial View

function setResult(html) {
    $('#result').html(html);
}

function loadPartialView(path) {
    $('#result').text('読み込み中…');
    $.post(path, $('#form1').serialize())
     .done(function (data) { setResult(data); })
     .fail(function (data) { $('#result').text('サーバー エラー'); });
}

$('#form1').submit(function (event) {
    event.preventDefault();
    loadPartialView('/Home/Books/');
});

// JavaScript + Web API

function bookToTableRow(book) {
    return $('<tr>')
           .append($('<td><a href="' + book.Url + '"><img src="' + book.ImageUrl + '" alt="' + book.Title + '"></a>' + '</td>'))
           .append($('<td>' + '<a href="' + book.Url + '">' + book.Title + '</a>' + '</td>'))
           .append($('<td>&yen;' + book.Price + '</td>'))
           .append($('<td>' + book.ReleaseDate + '</td>'))
           .append($('<td>' + book.Publisher + '</td>'))
           .append($('<td>' + book.Authors + '</td>'));
}

function booksToTable(books) {
    let table = $('<table class="table table-striped">');
    let thead = $('<thead>').appendTo(table);
    let tr    = $('<tr>').appendTo(thead);
    tr.append($('<th></th>'        ));
    tr.append($('<th>タイトル</th>'));
    tr.append($('<th>価格</th>'    ));
    tr.append($('<th>発売日</th>'  ));
    tr.append($('<th>出版社</th>'  ));
    tr.append($('<th>著者</th>'    ));

    let tbody = $('<tbody>').appendTo(table);
    for (var index = 0; index < books.length; index++)
        tbody.append(bookToTableRow(books[index]));
    return table;
}

$('#form2').submit(function (event) {
    event.preventDefault();

    $('#result').text('読み込み中…');

    let searchText = decodeURIComponent($('#searchText').val());
    $.get('/api/HomeApi/' + searchText + '/')
     .done(function (json) { $('#result').html(booksToTable(json)); })
     .fail(function (data) { $('#result').text('サーバー エラー'); });
});
