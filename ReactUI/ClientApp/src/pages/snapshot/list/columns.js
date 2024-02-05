import ActionColumn from "../../../components/dataTable/ActionColumn";

export const columns = [
  {
    title: "AVM",
    dataIndex: "mallInfoName",
    key: "mallInfoName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Mağaza",
    dataIndex: "storeName",
    key: "storeName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Müşteri Sayısı",
    dataIndex: "customerCount",
    key: "customerCount",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Kasadaki Müşteri Sayısı",
    dataIndex: "customerInSalesCount",
    key: "customerInSalesCount",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Çalışan Sayısı",
    dataIndex: "workerCount",
    key: "workerCount",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Gözlem Tarihi",
    dataIndex: "snapshotDate",
    key: "snapshotDate",
    sorter: true,
    render: (text) => <a>{text.split("T")[0]}</a>,
  },
  {
    title: "İşlem",
    key: "action",
    render: (text, record) => (
      <ActionColumn query="snapshot" id={record.pkId} />
    ),
  },
];
