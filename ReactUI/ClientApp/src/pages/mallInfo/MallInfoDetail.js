import React from "react";
import { useHistory, useParams } from "react-router-dom";
import { Form, Input, InputNumber, Select, Button, notification } from "antd";
import { Card, Col } from "antd";
import { useQuery, useMutation, useQueryClient } from "react-query";
import api from "../../api";

const { Option } = Select;

function MallInfoDetail() {
  let history = useHistory();
  const [form] = Form.useForm();
  const { id } = useParams();

  const countryCode = Form.useWatch("countryCode", form);
  const cityCode = Form.useWatch("cityCode", form);
  const title = id ? "Avm Düzenle" : "Avm Ekle";

  const QmallInfo = useQuery(["getMall", "mallInfo", id], api.getOneById, {
    enabled: id !== undefined,
    staleTime: Infinity,
  });
  const mallInfoInitial = QmallInfo.data?.data;

  const QgetCountries = useQuery(["getCountries"], api.getCountries, {
    staleTime: Infinity,
  });
  const countries = QgetCountries.data?.data ?? [];

  const QgetTowns = useQuery(["getTowns", cityCode], api.getTowns, {
    staleTime: Infinity,
    enabled: cityCode !== undefined,
  });
  const towns = QgetTowns.data?.data ?? [];

  const QgetCities = useQuery(["getCities", countryCode], api.getCities, {
    staleTime: Infinity,
    enabled: countryCode !== undefined,
  });
  const cities = QgetCities.data?.data ?? [];

  const queryClient = useQueryClient();
  const saveMall = useMutation(api.save, {
    onSuccess: (res) => {
      const description = id
        ? "Kayıt başarıyla güncellendi"
        : "Kayıt başarıyla eklendi";
      if (res.status === 201 || res.status === 204) {
        notification["success"]({
          message: "Başarılı",
          description: description,
        });
        history.push("/mall-info-list");
      }
      queryClient.invalidateQueries("saveMall");
    },
  });

  const onFinish = async (values) => {
    const method = id ? "PUT" : "POST";
    const customValues = id ? { ...values, pkId: Number(id) } : values;
    saveMall.mutate({ query: "mallInfo", data: customValues, method });
  };

  const handleClick = () => {
    history.push("/mall-info-list");
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

  if (id && !mallInfoInitial) {
    // create mode değil yani update mode'da ise ve mallInfoInitial henüz gelmediyse null dönsün ki
    return null; // Form componentinin initialValues'u undefined verilmesin yoksa data geldiğinde bile onları göremiyoruz componentte
  }

  return (
    <Col span={18}>
      <Card title={title} bordered={false}>
        <Form
          {...formItemLayout}
          form={form}
          initialValues={mallInfoInitial}
          name="mall_info_detail"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item
            name="countryCode"
            label="Ülke"
            rules={[
              {
                required: true,
                message: "Lütfen ülke ismini giriniz!",
              },
            ]}
          >
            <Select placeholder="Ülke seç">
              {countries.map((country) => (
                <Option value={country.code}>{country.name}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="cityCode"
            label="Şehir"
            rules={[
              {
                required: true,
                message: "Lütfen şehir ismini giriniz!",
              },
            ]}
            hasFeedback
          >
            <Select
              placeholder="Şehir seç"
              disabled={!form.getFieldValue("countryCode")}
            >
              {cities.map((city) => (
                <Option value={city.code}>{city.name}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="townCode"
            label="İlçe"
            rules={[
              {
                required: true,
                message: "Lütfen ilçe ismini giriniz!",
              },
            ]}
            hasFeedback
          >
            <Select
              placeholder="İlçe seç"
              disabled={!form.getFieldValue("cityCode")}
            >
              {towns.map((town) => (
                <Option value={town.code}>{town.name}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="mallName"
            label="AVM İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen AVM ismini giriniz!!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="leasableArea"
            label="Kiralanabilir Alan"
            rules={[
              {
                required: true,
                message: "Lütfen kiralanabilir alanı giriniz!",
              },
            ]}
          >
            <InputNumber
              style={{
                width: "100%",
              }}
            />
          </Form.Item>
          <Form.Item
            name="vehicleCapacity"
            label="Araç Kapasitesi"
            rules={[
              {
                required: true,
                message: "Lütfen araç kapasitesini giriniz!",
              },
            ]}
          >
            <InputNumber
              style={{
                width: "100%",
              }}
            />
          </Form.Item>
          <Form.Item {...tailFormItemLayout}>
            <Button type="seconder" onClick={handleClick}>
              Geri
            </Button>
            <Button type="primary" htmlType="submit">
              {id ? "Güncelle" : "Ekle"}
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </Col>
  );
}

export default MallInfoDetail;
