FROM node:18-alpine

RUN apk add bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib
RUN apk add libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/

RUN mkdir -p /usr/share/dotnet && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

RUN wget https://dot.net/v1/dotnet-install.sh
RUN chmod +x dotnet-install.sh

RUN ./dotnet-install.sh -c 6.0 --install-dir /usr/share/dotnet

WORKDIR /app
COPY . .

RUN npm ci

RUN npx nx run web:build:production

WORKDIR /app/dist/apps/web

ENV PORT=443
EXPOSE ${PORT}
RUN npm install --production
CMD npm run start
