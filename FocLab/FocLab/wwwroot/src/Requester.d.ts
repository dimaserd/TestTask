declare class Requester {
    SendPostRequestWithAnimation<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
    static SendAjaxPostInner(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function, animations: boolean): void;
    Post<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
}
