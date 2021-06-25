var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalrServer")
    .build();
connection.start();

connection.on("LoadDataTable", function() {
    LoadStockTableData();
});

LoadStockTableData();

function LoadStockTableData() {
    var tr = "";
    $.ajax({
        url: "/StockTable/GetDataTable",
        method: "GET",
        success: (result) => {
            $.each(result, (k, v) => {
                tr += `<tr> 
                <td>
                <a href="/StockTable/Edit?code=${
                  v.ma
                }" class="btn btn-sm btn-success">Sửa</a> 

                <form action="/StockTable/Delete" method="post">
                    <input type="hidden" value="${v.ma}" name="code" />
                    <input type="submit" style="cursor: pointer" onclick="return confirm('Bạn có chắc chắn muốn xóa??')" value="Xóa" class="btn btn-sm btn-danger" />
                </form>
                </td>
                <td>
                <a href="/StockTable/Details?code=${v.ma}">
                ${v.ma}
                </a> 
                </td>
                <td>${v.tc}</td>
                <td>${v.tran}</td>
                <td>${v.san}</td>
                <td>${v.muaG3 === null ? "" : v.muaG3}</td>
                <td>${v.muaKl3 === null ? "" : v.muaKl3}</td>
                <td>${v.muaG2 === null ? "" : v.muaG2}</td>
                <td>${v.muaKl2 === null ? "" : v.muaKl2}</td>
                <td>${v.muaG1 === null ? "" : v.muaG1}</td>
                <td>${v.muaKl1 === null ? "" : v.muaKl1}</td>
                <td>${v.khopLenhGia === null ? "" : v.khopLenhGia}</td>
                <td>${v.khopLenhKl === null ? "" : v.khopLenhKl}</td>
                <td>${v.tileTangGiam === null ? "" : v.tileTangGiam}</td>
                <td>${v.banG1 === null ? "" : v.banG1}</td>
                <td>${v.banKl1 === null ? "" : v.banKl1}</td>
                <td>${v.banG2 === null ? "" : v.banG2}</td>
                <td>${v.banKl2 === null ? "" : v.banKl2}</td>
                <td>${v.banG3 === null ? "" : v.banG3}</td>
                <td>${v.banKl3 === null ? "" : v.banKl3}</td>
                <td>${v.tongKl === null ? "" : v.tongKl}</td>
                <td>${v.moCua === null ? "" : v.moCua}</td>
                <td>${v.caoNhat === null ? "" : v.caoNhat}</td>
                <td>${v.thapNhat === null ? "" : v.thapNhat}</td>
                <td>${v.nnmua === null ? "" : v.nnmua}</td>
                <td>${v.nnban === null ? "" : v.nnban}</td>
                <td>${v.room === null ? "" : v.room}</td>
                </tr>`;
            });
            console.log(result);
            $("#tableBody").html(tr);
        },
        error: (error) => {
            console.log(error);
        },
    });
}