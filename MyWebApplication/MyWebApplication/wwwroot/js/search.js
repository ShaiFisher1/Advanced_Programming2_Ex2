$(function () {
    $('form').submit(async e => {
        e.preventDefault();

        const q = $('#search').val();

       // $('tbody').load('/RankingItems/Search3?query='+q);

        const response = await fetch('/RankingItems/Search3?query=' + q);
        const data = await response.json();

        console.log(data);

        $('tbody').html('<tr><td' + data[0].feedback + '</td></tr>');

    })
});

