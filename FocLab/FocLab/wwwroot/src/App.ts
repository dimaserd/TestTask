interface BaseApiResponse {
    IsSucceeded: boolean;
    Message: string;
}

interface HouseModel {
    Id: number;
    Address: string;
    WaterCounter: WaterCounterModel;
}

interface WaterCounterModel {
    Id: number;
    CurrentIndication: number;
    FactoryNumber: string;
}

interface CreateHouse {
    Address: string;
}

interface CreateWaterCounter {
    HouseId: number;
    FactoryNumber: string;
}

class App {

    constructor() {
        this.GetHouses();
        this.SetHandlers();
    }

    SetHandlers(): void {
        document.getElementById("create-house-btn").addEventListener("click", this.CreateHouse.bind(this));

        document.getElementById("create-water-counter-btn").addEventListener("click", this.CreateWaterCounter.bind(this))

        //Вешаю обработчик на все кнопки создания счетчика к дому
        $(document).on("click", `.${App.CreateWaterCounterBtnClass}`, e =>
        {
            let attrValue = (e.target as HTMLElement).getAttribute(App.DataHouseAttribute);

            this.CurrentHouseId = Number(attrValue);

            $("#create-water-counter-modal").modal('show');
        });
    }

    /*
     * Дом к которому создается счетчик воды
     */
    CurrentHouseId: number;

    Requester = new Requester();

    /*
     *  Класс для кнопок создания счетчика воды
     */
    static CreateWaterCounterBtnClass = "btn-create-water-counter";

    static DataHouseAttribute = "data-house-id";

    GetWaterCounterHtml(data: HouseModel) {
        if (data.WaterCounter == null) {
            return `<button ${App.DataHouseAttribute}="${data.Id}" class="btn btn-primary ${App.CreateWaterCounterBtnClass}">Создать счетчик</button>`;
        }

        return `Счетчик воды. <br/> Заводской номер: ${data.WaterCounter.FactoryNumber} <br/> Текущие показания: ${data.WaterCounter.CurrentIndication}`;
    }

    GetHousesHandler(data: Array<HouseModel>): void {

        console.log("GetHousesHandler", data);

        //запускаю обновление показаний счетчиков
        this.Requester.Post('/Api/House/AddIndications', {}, null, null);

        let html = '';

        data.forEach(x => {
            html += `<tr>
                <td>${x.Id}</td>
                <td>${x.Address}</td>
                <td>${this.GetWaterCounterHtml(x)}</td>
            </tr>`;
        })

        document.getElementById("houses-table-body").innerHTML = html;
    }

    GetHouses(): void {
        this.Requester.Post('/Api/House/Get', {}, this.GetHousesHandler.bind(this), null);
    }

    CreateHouseOrWaterCounterHandler(data: BaseApiResponse): void {
        alert(data.Message);

        if (data.IsSucceeded) {
            setTimeout(() => location.reload(), 500);
        }
    }

    CreateHouse(): void {
        let data: CreateHouse = {
            Address: (document.getElementById("create-house-address") as HTMLInputElement).value
        };

        this.Requester.Post('/Api/House/Create', data, this.CreateHouseOrWaterCounterHandler, null);

        $('.modal').modal('hide');
    }

    CreateWaterCounter(): void {
        let data: CreateWaterCounter = {
            HouseId: this.CurrentHouseId,
            FactoryNumber: (document.getElementById("create-water-counter-factory-number") as HTMLInputElement).value
        };

        this.Requester.Post('/Api/House/AddWaterCounter', data, this.CreateHouseOrWaterCounterHandler, null);

        $('.modal').modal('hide');
    }
}

new App();