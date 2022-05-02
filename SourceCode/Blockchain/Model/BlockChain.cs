using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Model
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        //public Transaction Data { get; set; }
        public Transaction Data = new Transaction("","",0,"");
        public int Nonce { get; set; } = 0;

        public Block(DateTime timeStamp, string previousHash, Transaction data)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;

            Data.Sender = data.Sender;
            Data.Receiver = data.Receiver;
            Data.Amount = data.Amount;
            Data.Miner = data.Miner;


            Hash = CalculateHash();
        }

        public Block(DateTime timeStamp, string previousHash, Transaction data, int nonce)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;

            Data.Sender = data.Sender;
            Data.Receiver = data.Receiver;
            Data.Amount = data.Amount;
            Data.Miner = data.Miner;


            Hash = CalculateHash();
            Nonce = nonce;
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data.Sender+Data.Receiver+Data.Miner+Data.Amount.ToString()}-{Nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }

    public static class MyBlockChain
    {
        public static BlockChain blockChain = new BlockChain();
    }

    
    public class BlockChain
    {
        public IList<Block> Chain { set; get; }
        public int Difficulty { set; get; } = 2;

        public BlockChain()
        {
            InitializeChain();
            AddGenesisBlock();
        }


        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            Transaction transaction = new Transaction("", "", 0, "");
            return new Block(DateTime.Now, null, transaction);
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(this.Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

        public void CopyToBlockBindingList(BindingList<Block> blockList)
        {
            for (int i = 0; i < Chain.Count; i++)
            {
                Block block = new Block(Chain[i].TimeStamp, Chain[i].PreviousHash, Chain[i].Data, Chain[i].Nonce);
                blockList.Add(block);
            }
        }
    }


}
