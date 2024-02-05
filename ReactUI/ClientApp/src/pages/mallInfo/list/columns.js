import ActionColumn from "../../../components/dataTable/ActionColumn";

export const columns = [
  {
    title: "Avm İsmi",
    dataIndex: "mallName",
    key: "mallName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Ülke",
    dataIndex: "countryName",
    key: "countryName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Şehir",
    dataIndex: "cityName",
    key: "cityName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "İlçe",
    dataIndex: "townName",
    key: "townName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Kiralanabilir Alan",
    dataIndex: "leasableArea",
    key: "leasableArea",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Araç Kapasitesi",
    dataIndex: "vehicleCapacity",
    key: "vehicleCapacity",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "İşlem",
    key: "action",
    render: (text, record) => (
      <ActionColumn query="mallInfo" id={record.pkId} path="mall-info" />
    ),
  },
];
