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
declare class App {
    constructor();
    SetHandlers(): void;
    CurrentHouseId: number;
    Requester: Requester;
    static CreateWaterCounterBtnClass: string;
    static DataHouseAttribute: string;
    GetWaterCounterHtml(data: HouseModel): string;
    GetHousesHandler(data: Array<HouseModel>): void;
    GetHouses(): void;
    CreateHouseOrWaterCounterHandler(data: BaseApiResponse): void;
    CreateHouse(): void;
    CreateWaterCounter(): void;
}
