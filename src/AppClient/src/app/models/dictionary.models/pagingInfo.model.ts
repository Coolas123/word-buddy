export class PagingInfo{
    constructor(
        public pageSize: number,
        public totalItems: number=0,
        public currentPage: number=0,
    ){}
    public TotalPages():number {
        return Math.ceil(this.totalItems / this.pageSize)
    }
}