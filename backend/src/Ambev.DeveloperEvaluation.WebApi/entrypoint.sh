#!/bin/bash
set -e

host="$DB_HOST"
port="$DB_PORT"

echo "⏳ Aguardando banco $host:$port..."

until nc -z "$host" "$port"; do
  echo "⏱️  Aguardando conexão com banco..."
  sleep 2
done

echo "✅ Banco disponível. Subindo aplicação..."
exec "$@"
