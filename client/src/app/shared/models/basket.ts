import * as cuid2 from "@paralleldrive/cuid2";

export interface BasketItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export interface Basket {
    id: string;
    items: BasketItem[];
}

export class Basket implements Basket {
    id = cuid2.createId();
    items: BasketItem[] = [];
}

export interface BasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}
