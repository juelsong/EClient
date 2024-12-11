export declare interface IReply<T> {
    code: number;
    data: T;
    message: string;
    success: boolean;
    timestamp: Date;
}

export declare type BooleanReply = IReply<boolean>;
export declare type StringReply = IReply<string>;
