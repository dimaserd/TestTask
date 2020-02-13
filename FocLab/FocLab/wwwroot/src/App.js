var App = /** @class */ (function () {
    function App() {
        this.Requester = new Requester();
        this.GetHouses();
        this.SetHandlers();
    }
    App.prototype.SetHandlers = function () {
        var _this = this;
        document.getElementById("create-house-btn").addEventListener("click", this.CreateHouse.bind(this));
        document.getElementById("create-water-counter-btn").addEventListener("click", this.CreateWaterCounter.bind(this));
        //Вешаю обработчик на все кнопки создания счетчика к дому
        $(document).on("click", "." + App.CreateWaterCounterBtnClass, function (e) {
            var attrValue = e.target.getAttribute(App.DataHouseAttribute);
            _this.CurrentHouseId = Number(attrValue);
            $("#create-water-counter-modal").modal('show');
        });
    };
    App.prototype.GetWaterCounterHtml = function (data) {
        if (data.WaterCounter == null) {
            return "<button " + App.DataHouseAttribute + "=\"" + data.Id + "\" class=\"btn btn-primary " + App.CreateWaterCounterBtnClass + "\">\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u0441\u0447\u0435\u0442\u0447\u0438\u043A</button>";
        }
        return "\u0421\u0447\u0435\u0442\u0447\u0438\u043A \u0432\u043E\u0434\u044B. <br/> \u0417\u0430\u0432\u043E\u0434\u0441\u043A\u043E\u0439 \u043D\u043E\u043C\u0435\u0440: " + data.WaterCounter.FactoryNumber + " <br/> \u0422\u0435\u043A\u0443\u0449\u0438\u0435 \u043F\u043E\u043A\u0430\u0437\u0430\u043D\u0438\u044F: " + data.WaterCounter.CurrentIndication;
    };
    App.prototype.GetHousesHandler = function (data) {
        var _this = this;
        console.log("GetHousesHandler", data);
        //запускаю обновление показаний счетчиков
        this.Requester.Post('/Api/House/AddIndications', {}, null, null);
        var html = '';
        data.forEach(function (x) {
            html += "<tr>\n                <td>" + x.Id + "</td>\n                <td>" + x.Address + "</td>\n                <td>" + _this.GetWaterCounterHtml(x) + "</td>\n            </tr>";
        });
        document.getElementById("houses-table-body").innerHTML = html;
    };
    App.prototype.GetHouses = function () {
        this.Requester.Post('/Api/House/Get', {}, this.GetHousesHandler.bind(this), null);
    };
    App.prototype.CreateHouseOrWaterCounterHandler = function (data) {
        alert(data.Message);
        if (data.IsSucceeded) {
            setTimeout(function () { return location.reload(); }, 500);
        }
    };
    App.prototype.CreateHouse = function () {
        var data = {
            Address: document.getElementById("create-house-address").value
        };
        this.Requester.Post('/Api/House/Create', data, this.CreateHouseOrWaterCounterHandler, null);
        $('.modal').modal('hide');
    };
    App.prototype.CreateWaterCounter = function () {
        var data = {
            HouseId: this.CurrentHouseId,
            FactoryNumber: document.getElementById("create-water-counter-factory-number").value
        };
        this.Requester.Post('/Api/House/AddWaterCounter', data, this.CreateHouseOrWaterCounterHandler, null);
        $('.modal').modal('hide');
    };
    /*
     *  Класс для кнопок создания счетчика воды
     */
    App.CreateWaterCounterBtnClass = "btn-create-water-counter";
    App.DataHouseAttribute = "data-house-id";
    return App;
}());
new App();
