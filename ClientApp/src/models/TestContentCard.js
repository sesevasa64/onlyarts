import ContentCard from "./ContentCard.js";

const t1 = new ContentCard("Vsevolod", 
  "https://gottadotherightthing.com/wp-content/uploads/2018/12/beautiful-blur-blurred-background-733872-1.jpg",
  "https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg",
  "Aboba",
  'Bla bla bla bla bla',
  ["https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg", 
  "https://cdn-ru0.puzzlegarage.com/img/puzzle/5/5765_preview_r.v1.jpg",
  "https://avatars.mds.yandex.net/get-zen_doc/1880741/pub_60ebc44a0f1e1b2a8ceb58e7_60ebc559b56ded7a70c250ed/scale_1200"]
);

let t2, t3, t4;
t2 = Object.assign({}, t1);
t2.user_name = "Artem";
t2.facial_image = "https://vsrap.ru/wp-content/uploads/2020/08/bez-imeni-1-7.jpg"
t2.caption = "Second Aboba"; 
t3 = Object.assign({}, t1);
t3.user_name = "Leha";
t3.facial_image = "https://i.ytimg.com/vi/Lp6Xy_PiACI/maxresdefault.jpg";
t3.caption = "Fuck Aboba";
t4 = Object.assign({}, t1);
t4.user_name = "Unus";
t4.facial_image = "https://naked-science.ru/wp-content/uploads/2016/10/images_11619502663_94c604221c_o.jpg"
t4.caption = "Third Aboba";

export {t1, t2, t3, t4};
