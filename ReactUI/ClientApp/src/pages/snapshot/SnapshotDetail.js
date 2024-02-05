import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Form, Input, DatePicker, Select, Button, notification } from "antd";
import { Card, Col } from "antd";
import { useQuery, useMutation, useQueryClient } from "react-query";
import api from "../../api";
import moment from "moment"; // date.clone hatası

function SnapshotDetail() {
  let history = useHistory();
  const [form] = Form.useForm();
  const { id } = useParams();
  const [mallInfoName, setMallInfoName] = useState();
  const { Option } = Select;
  const title = id ? "Gözlemi Düzenle" : "Yeni Gözlem Ekle";

  const mallInfoId = Form.useWatch("mallInfoId", form);

  const Qsnapshot = useQuery(["getSnapshot", "snapshot", id], api.getOneById, {
    enabled: id !== undefined,
    staleTime: Infinity,
  });
  let snapshotInitial = Qsnapshot.data?.data;

  const QgetMallInfosQuery = useQuery(
    ["getMallInfos", "mallInfo"],
    api.getList,
    {
      staleTime: Infinity,
    }
  );
  const mallInfos = QgetMallInfosQuery.data?.data ?? [];

  const QgetStoresQuery = useQuery(
    [
      "getStores",
      "store",
      undefined,
      undefined,
      undefined,
      mallInfoName === "" ? undefined : mallInfoName,
    ],
    api.getList,
    {
      enabled: mallInfoId !== undefined,
      staleTime: Infinity,
    }
  );
  const stores = QgetStoresQuery.data?.data ?? [];

  useEffect(() => {
    setMallInfoName(snapshotInitial?.storeName);
  }, [snapshotInitial]);

  const queryClient = useQueryClient();
  const saveSnapshot = useMutation(api.save, {
    onSuccess: (res) => {
      const description = id
        ? "Kayıt başarıyla güncellendi"
        : "Kayıt başarıyla eklendi";
      if (res.status === 201 || res.status === 204) {
        notification["success"]({
          message: "Başarılı",
          description: description,
        });
        form.resetFields([
          "storeId",
          "customerCount",
          "customerInSalesCount",
          "workerCount",
          "snapshotDate",
        ]);
      }
      queryClient.invalidateQueries("saveSnapshot");
    },
  });

  const onFinish = async (values) => {
    const method = id ? "PUT" : "POST";
    const customValues = id ? { ...values, pkId: Number(id) } : values;
    saveSnapshot.mutate({ query: "snapshot", data: customValues, method });
  };

  const handleClick = () => {
    history.push("/snapshot-list");
  };

  const formItemLayout = {
    labelCol: {
      xs: {
        span: 24,
      },
      sm: {
        span: 8,
      },
    },
    wrapperCol: {
      xs: {
        span: 24,
      },
      sm: {
        span: 16,
      },
    },
  };
  const tailFormItemLayout = {
    wrapperCol: {
      xs: {
        span: 24,
        offset: 0,
      },
      sm: {
        span: 16,
        offset: 8,
      },
    },
  };

  if (id && !snapshotInitial) {
    return null;
  }

  snapshotInitial = {
    ...snapshotInitial,
    snapshotDate: moment(snapshotInitial?.snapshotDate),
  };

  return (
    <Col span={18}>
      <Card title={title} bordered={false}>
        <Form
          {...formItemLayout}
          form={form}
          initialValues={snapshotInitial}
          name="snapshot_detail"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item
            name="mallInfoId"
            label="Avm İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen AVM ismi giriniz!",
              },
            ]}
          >
            <Select
              placeholder="AVM seç"
              onChange={(_, event) => {
                form.setFieldsValue({ ["storeId"]: "" });
                setMallInfoName(event.children);
              }}
            >
              {mallInfos.map((mall) => (
                <Option value={mall.pkId}>{mall.mallName}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="storeId"
            label="Mağaza İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen mağaza ismi giriniz!",
              },
            ]}
          >
            <Select
              placeholder="Mağaza seç"
              disabled={!form.getFieldValue("mallInfoId")}
            >
              {stores.map((store) => (
                <Option value={store.pkId}>{store.storeName}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="customerCount"
            label="Müşteri Sayısı"
            rules={[
              {
                required: true,
                message: "Lütfen müşteri sayısını giriniz!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="customerInSalesCount"
            label="Kasadaki Müşteri Sayısı"
            rules={[
              {
                required: true,
                message: "Lütfen kasadaki müşteri sayısını giriniz!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="workerCount"
            label="Çalışan Sayısı"
            rules={[
              {
                required: true,
                message: "Lütfen çalışan sayısını giriniz!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          {id && (
            <Form.Item
              name="snapshotDate"
              label="Gözlem Tarihi"
              rules={[
                {
                  required: true,
                  message: "Lütfen gözlem tarihini giriniz!",
                },
              ]}
            >
              <DatePicker style={{ width: "100%" }} disabled />
            </Form.Item>
          )}
          <Form.Item {...tailFormItemLayout}>
            <Button type="seconder" onClick={handleClick}>
              Geri
            </Button>
            <Button type="primary" htmlType="submit">
              {id ? "Güncelle" : "Ekle & Yeni Gözlem"}
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </Col>
  );
}

export default SnapshotDetail;
